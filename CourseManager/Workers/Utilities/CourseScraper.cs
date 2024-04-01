using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using Data.Entities;
using Data.Enums;
using Repositories.Interfaces;
using Scrapers.ScrapingUtilities;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace Scrapers.Utilities;

public class GlobalContext
{
    public Faculty? Faculty { get; set; }
    public CourseOffering? Offering { get; set; }
    public Section? Section { get; set; }
}

public static class CourseScraper
{
    public static bool IsSummerTerm = false;
    public static IServiceProvider? ServiceProvider { get; set; } = null;
    private static GlobalContext Context { get; set; } = new GlobalContext();

    public static async Task<List<Faculty>> ScrapeFaculties()
    {
        var configuration =
            (ServiceProvider ?? throw new InvalidOperationException()).GetRequiredService<IConfiguration>();
        // Get a new cookie
        var cookie = await CookieManager.GetCookie(configuration);

        // create new http client for the request to pass in headers
        var client = new HttpClient();
        client.DefaultRequestHeaders.Add("Cookie", cookie.Value);

        var response = await client.GetAsync(IsSummerTerm
            ? configuration["Scraper:SummerBuilderUrl"]
            : configuration["Scraper:BuilderUrl"]);
        var html = await response.Content.ReadAsStringAsync();

        if (string.IsNullOrWhiteSpace(html)) throw new Exception("No html was returned from the request");

        // parse the HTML string to DOM nodes for AngleSharp
        var config = Configuration.Default;
        var context = BrowsingContext.New(config);
        var document = await context.OpenAsync(req => req.Content(html));

        if (document == null) throw new Exception("No document was returned from the request");

        var faculties = new List<Faculty>();
        var options = document.QuerySelectorAll("select#Subject > option");

        foreach (var option in options)
        {
            var abbreviation = option.GetAttribute("value");
            var name = option.TextContent.Trim();

            // Skip the first option as it is just a placeholder
            if (!string.IsNullOrWhiteSpace(abbreviation))
            {
                faculties.Add(new Faculty()
                {
                    Name = name,
                    Abbreviation = abbreviation,
                });
            }
        }

        return faculties;
    }

    private static async Task<IDocument> OpenFacultyDocument(Faculty faculty,
        List<KeyValuePair<string, string>> sectionTypes)
    {
        var configuration =
            (ServiceProvider ?? throw new InvalidOperationException()).GetRequiredService<IConfiguration>();

        // get cookies
        var cookie = await CookieManager.GetCookie(configuration);

        // create new http client for the request to pass in headers
        var client = new HttpClient();
        client.DefaultRequestHeaders.Add("Cookie", cookie.Value);

        var requestData = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("version_id", ""),
            new KeyValuePair<string, string>("Subject", faculty.Abbreviation),
            new KeyValuePair<string, string>("delivery_type", "All"),
            new KeyValuePair<string, string>("catalog_nbr_typed", ""),
            new KeyValuePair<string, string>("catalog_nbr", ""),
            new KeyValuePair<string, string>("day", "m"),
            new KeyValuePair<string, string>("day", "tu"),
            new KeyValuePair<string, string>("day", "w"),
            new KeyValuePair<string, string>("day", "th"),
            new KeyValuePair<string, string>("day", "f"),
            new KeyValuePair<string, string>("Designation", "Any"),
            new KeyValuePair<string, string>("start_time", ""),
            new KeyValuePair<string, string>("end_time", ""),
            new KeyValuePair<string, string>("command_search", "search")
        }.Concat(sectionTypes));

        var response =
            await client.PostAsync(
                IsSummerTerm ? configuration["Scraper:SummerBuilderUrl"] : configuration["Scraper:BuilderUrl"],
                requestData);
        var html = await response.Content.ReadAsStringAsync();

        if (string.IsNullOrWhiteSpace(html)) throw new Exception("No html was returned from the request");

        // parse the HTML string to DOM nodes for AngleSharp
        var config = Configuration.Default;
        var context = BrowsingContext.New(config);

        var document = await context.OpenAsync(req => req.Content(html));
        if (document == null) throw new Exception("No document was returned from the request");

        return document;
    }

    private static Course ScrapeCourse(IElement courseHeader)
    {
        var descriptionElement = courseHeader.NextElementSibling;
        if (descriptionElement == null) throw new Exception("Could not find description element");

        var description = descriptionElement.TextContent.Trim();
        Console.WriteLine(courseHeader.TextContent);

        var courseName = courseHeader.TextContent.Split(" - ")[1].Trim();
        var courseNumber = courseHeader.TextContent.Split(" - ")[0].Split(" ")[1].Trim();

        // if last character is a letter, remove it
        if (char.IsLetter(courseNumber[^1])) courseNumber = courseNumber.Substring(0, courseNumber.Length - 1);

        if (!int.TryParse(courseNumber, out var parsedCourseNumber))
            throw new Exception("Could not parse course number");

        // get internal course id
        var htmlElement = descriptionElement.NextElementSibling;
        while (htmlElement != null && htmlElement.TagName != "TABLE") htmlElement = htmlElement.NextElementSibling;
        var tableElement = htmlElement as IHtmlTableElement;
        if (tableElement == null) throw new Exception("Could not find table element");

        var inputElement = tableElement.QuerySelector("input[name=crse_id]");
        if (inputElement == null)
        {
            Console.WriteLine(courseName);
            throw new Exception("Could not find input element");
        }

        // set internal course id to the value of the input element
        var internalCourseId = inputElement.GetAttribute("value");
        if (string.IsNullOrWhiteSpace(internalCourseId)) throw new Exception("Could not find internal course id");

        var course = new Course()
        {
            Number = parsedCourseNumber,
            Name = courseName,
            Description = description,
            InternalCourseId = internalCourseId
        };

        return course;
    }

    private static Suffix GetSuffix(IElement courseHeader)
    {
        var courseNumber = courseHeader.TextContent.Split(" - ")[0].Split(" ")[1];

        // suffix is either at end of course number or not present
        // check if last character is a letter
        if (!char.IsLetter(courseNumber[^1])) return Suffix.NoSuffix;

        var suffixChar = courseNumber[^1];
        // try to convert to suffix enum
        if (Enum.TryParse<Suffix>(suffixChar.ToString(), out var suffix)) return suffix;

        throw new Exception("Could not parse suffix");
    }

    private static TimingDetails ScrapeTimingDetail(IHtmlTableRowElement row)
    {
        var cells = row.Cells;

        // ex. <td width="34%>M &nbsp; W Th F </td>
        // 1. get days of week bitmap; split by " " and filter out &nbsp; then convert to DaysOfWeek enum
        var daysOfWeekStrings = cells[0].InnerHtml.Trim().Split(" ").Where(x => x != "&nbsp;");
        var daysOfWeek = new List<DayOfWeek>();
        foreach (var dayOfWeekString in daysOfWeekStrings)
        {
            var dayOfWeek = dayOfWeekString switch
            {
                "M" => DayOfWeek.Monday,
                "Tu" => DayOfWeek.Tuesday,
                "W" => DayOfWeek.Wednesday,
                "Th" => DayOfWeek.Thursday,
                "F" => DayOfWeek.Friday,
                "Sa" => DayOfWeek.Saturday,
                "Su" => DayOfWeek.Sunday,
                _ => throw new Exception($"Could not parse day of week: {dayOfWeekString}")
            };
            daysOfWeek.Add(dayOfWeek);
        }

        // Map Monday to first bit, Tuesday to second bit, etc.
        var daysOfWeekToBitmapIndex = new Dictionary<DayOfWeek, int>
        {
            { DayOfWeek.Monday, 0 },
            { DayOfWeek.Tuesday, 1 },
            { DayOfWeek.Wednesday, 2 },
            { DayOfWeek.Thursday, 3 },
            { DayOfWeek.Friday, 4 },
            { DayOfWeek.Saturday, 5 },
            { DayOfWeek.Sunday, 6 }
        };

        // convert days of week to bitmap
        var daysOfWeekBits = new List<char> { '0', '0', '0', '0', '0', '0', '0' };
        foreach (var dayOfWeek in daysOfWeek) daysOfWeekBits[daysOfWeekToBitmapIndex[dayOfWeek]] = '1';
        var daysOfWeekBitmap = new string(daysOfWeekBits.ToArray());

        // 2. get time
        var time = cells[1].TextContent.Trim() ?? string.Empty;
        if (time == "-") time = string.Empty;

        return new TimingDetails
        {
            DaysOfWeekBitmap = daysOfWeekBitmap,
            Time = time,
            Location = cells[2].TextContent.Trim()
        };
    }

    private static Section ScrapeSection(IHtmlTableRowElement row)
    {
        var cells = row.Cells;

        // 1. get component type string
        var componentTypeString = cells[0].TextContent.Trim();
        var component = componentTypeString switch
        {
            "LEC" => ComponentType.Lecture,
            "TUT" => ComponentType.Tutorial,
            "LAB" => ComponentType.Lab,
            _ => throw new Exception($"Could not parse component type: {componentTypeString}")
        };

        // 2. get section number
        var sectionNumberString = cells[1].TextContent.Trim();
        if (!int.TryParse(sectionNumberString, out var sectionNumber))
            throw new Exception($"Could not parse section number: {sectionNumberString}");

        // 3. get class number
        var classNumberString = cells[2].TextContent.Trim();
        if (!int.TryParse(classNumberString, out var classNumber))
            throw new Exception($"Could not parse class number: {classNumberString}");

        // 4. get instructors (they are separated by <br> tags so split based on the html)
        var instructorNames = cells[3].InnerHtml.Split("<br>").Select(x => x.Trim()).ToList();

        // there is a trailing <br> tag so remove the last element
        instructorNames.RemoveAt(instructorNames.Count - 1);

        // 5. get timetable requisite string
        var timetableRequisiteString = cells[4].TextContent.Trim();

        // 6. Section location and times
        var sectionLocationAndTimesTable = cells[5].QuerySelector("table") as IHtmlTableElement;
        if (sectionLocationAndTimesTable == null)
            throw new Exception("Could not find section location and times table");

        var timingDetails = new List<TimingDetails>();
        foreach (var sectionLocationAndTimesRow in sectionLocationAndTimesTable.Rows)
        {
            var timingDetail = ScrapeTimingDetail(sectionLocationAndTimesRow);
            timingDetails.Add(timingDetail);
        }

        // Get the timing details string in the span within cells[5]
        var timingDetailsSpan = cells[5].QuerySelector("span") as IHtmlSpanElement;
        var timingDetailsText = timingDetailsSpan?.TextContent.Trim();

        // 7. get status
        var statusString = cells[7].TextContent.Trim();
        var status = statusString switch
        {
            "Full" => StatusType.Full,
            "Not Full" => StatusType.NotFull,
            _ => throw new Exception($"Could not parse section status: {statusString}")
        };

        // 8. get waitList size
        var waitListSizeString = cells[8].TextContent.Trim();
        if (!int.TryParse(waitListSizeString, out var waitListSize))
            throw new Exception($"Could not parse waitList size: {waitListSizeString}");

        var validCampusNames = new List<string> { "Main", "King's", "Huron", "Brescia", "" };

        // 9. check if campus has been moved over (section is inbetween in the summer schdule)
        var tenthCellContents = cells[10].TextContent.Trim();
        var offset = 0;
        if (validCampusNames.Contains(tenthCellContents)) offset = 1;

        var campusString = cells[9 + offset].TextContent.Trim();
        var campus = campusString switch
        {
            "Main" => Campus.Main,
            "King's" => Campus.King,
            "Huron" => Campus.Huron,
            "Brescia" => Campus.Brescia,
            "" => Campus.Unknown,
            _ => throw new Exception($"Could not parse campus: {campusString}")
        };

        // 10. get delivery type
        var deliveryTypeString = cells[10 + offset].TextContent.Trim();
        var deliveryType = deliveryTypeString switch
        {
            "In Person" => DeliveryType.InPerson,
            "Distance Studies/Online" => DeliveryType.Online,
            "Blended" => DeliveryType.Hybrid,
            "Other" => DeliveryType.Other,
            _ => throw new Exception($"Could not parse delivery type: {deliveryTypeString}")
        };

        var sectionParams = new SectionConstructorParams
        {
            ComponentType = component,
            Number = sectionNumber,
            ClassNumber = classNumber,
            ProfessorNames = instructorNames,
            TimetableRequisiteString = timetableRequisiteString,
            TimingDetails = timingDetails,
            TimingDetailsText = timingDetailsText ?? string.Empty,
            Status = status,
            WaitListSize = waitListSize,
            Campus = campus,
            Delivery = deliveryType,
            CourseOfferingId = Context.Offering?.Id ?? throw new InvalidOperationException("Missing course offering in context")
        };

        var section = new Section(sectionParams);
        
        return section;
    }

    private static async Task<Section> ControlScrapedSection(IHtmlTableRowElement row)
    {
        if (ServiceProvider == null) throw new InvalidOperationException();
        var sectionRepository = ServiceProvider.GetRequiredService<ISectionRepository>();

        var section = ScrapeSection(row);
        Context.Section = section;
        
        await sectionRepository.InsertAsync(section);
        foreach (var professorName in section.ProfessorNames)
        {
            if (string.IsNullOrWhiteSpace(professorName)) continue;

            // instructor name follows format first initial + period + last name e.g. J. Ali-Hassan
            var splitName = professorName.Split(".");
            if (splitName.Length != 2) throw new Exception($"Could not parse instructor name: {professorName}");

            var firstName = splitName[0].Trim();
            var lastName = splitName[1].Trim();

            Console.WriteLine($"Processed name: {firstName} {lastName}");

            // Make a request to the directory to get the professor
            var possibleProfessors = await DirectoryScraper.GetProfessorsInDirectory(firstName, lastName,
                DirectoryScraper.SearchOption.StartsWith);

            // handle this case with better searching (e.g. contains + NLP)
            if (possibleProfessors.Count == 0)
            {
                Console.WriteLine($"Could not find any matching professors with name: {firstName} {lastName}");
            }

            if (possibleProfessors.Count == 1)
            {
                var directoryProfessor = possibleProfessors.First();
                
               await UpsertDirectoryProfessor(directoryProfessor);
            }


            if (possibleProfessors.Count > 1)
            {
                var professorOptionsString = string.Join(";", possibleProfessors.Select(x => x.Name + " " + x.Departments + " " + x.Email));
                var professorInformation = $"Name: {firstName} {lastName}; Department: {Context?.Faculty?.Name ?? "Missing faculty in context"}";
                var linkedProfessor = await ProfLinkerHelper.LinkProfessor(professorInformation, professorOptionsString);
                
                Console.WriteLine("Linked professor: " + linkedProfessor.ProfessorName + " " + linkedProfessor.Email);
                
                var matchingProfessor = possibleProfessors.FirstOrDefault(x => x.Email == linkedProfessor.Email);
                if (matchingProfessor == null)
                {
                    Console.WriteLine("Could not find matching professor");
                    continue;
                }
                
                await UpsertDirectoryProfessor(matchingProfessor);
            }
        }

        return section;
    }

    // returns existing professor if already in db otherwise returns the newly inserted professor
    private static async Task<Professor> UpsertDirectoryProfessor(DirectoryScraper.DirectoryProfessor directoryProfessor)
    {
        var professorRepository = (ServiceProvider ?? throw new InvalidOperationException()).GetRequiredService<IProfessorRepository>();
        var scrapedProfessor = new Professor
        {
            Email = directoryProfessor.Email,
            Name = directoryProfessor.Name,
            UwoId = directoryProfessor.UwoId
        };
        
        var existingProfessor = await professorRepository.GetSingleOrDefaultAsync(x => x.UwoId == scrapedProfessor.UwoId);
        
        if (existingProfessor == null)
        {
            scrapedProfessor.Sections.Add(Context.Section ?? throw new InvalidOperationException("Missing section in context"));
            await professorRepository.InsertAsync(scrapedProfessor);
        }
        else
        {
            existingProfessor.Sections.Add(Context.Section ?? throw new InvalidOperationException("Missing section in context"));
            await professorRepository.UpdateAsync(existingProfessor);
        }
        
        return existingProfessor ?? scrapedProfessor;
    }

    private static async Task<List<Section>> ScrapeOfferingSections(IElement courseHeader)
    {
        var sections = new List<Section>();

        var htmlElement = courseHeader.NextElementSibling;
        while (htmlElement != null && htmlElement.TagName != "TABLE") htmlElement = htmlElement.NextElementSibling;
        var tableElement = htmlElement as IHtmlTableElement;
        if (tableElement == null) throw new Exception("Could not find table element");

        foreach (var row in tableElement.Rows.Skip(1))
        {
            var section = await ControlScrapedSection(row);
            sections.Add(section);
        }

        return sections;
    }

    private static bool DidSearchSucceed(IDocument page)
    {
        // <div class="alert alert-warning" role="alert"> Unable to display your search results as it exceeds 300 courses. Please refine your search. </div>
        var alertElement = page.QuerySelector("div.alert.alert-warning");
        if (alertElement == null) return true;

        if (alertElement.InnerHtml.Contains(
                "Unable to display your search results as it exceeds 300 courses. Please refine your search."))
            return false;

        return true;
    }

    private static async Task<List<Course>> PopulateCoursesInDocument(IDocument document, Faculty faculty)
    {
        var courseRepository = (ServiceProvider ?? throw new InvalidOperationException())
            .GetRequiredService<ICourseRepository>();
        var courseOfferingRepository = ServiceProvider.GetRequiredService<ICourseOfferingRepository>();
        
        Context.Faculty = faculty;

        var courses = new List<Course>();

        var selectElement = document.QuerySelector("#select_term");
        if (selectElement == null) throw new Exception("Could not find select element");

        // get selected option
        var selectedOption = selectElement.QuerySelector("option[selected]");
        if (selectedOption == null) throw new Exception("Could not find selected option");

        // get the internal term id from the value attribute
        var termId = selectedOption.GetAttribute("value");
        if (!int.TryParse(termId, out var parsedTermId)) throw new Exception("Could not parse term id");

        // get the year and calendar source from the selected option inner text in the format "Fall Winter YEAR" or "Summer YEAR"
        var termString = selectedOption.TextContent.Trim();
        var year = int.Parse(termString.Split(" ").Last());
        var calendarSource = termString.Split(" ")[..^1].Aggregate((x, y) => x + " " + y);

        // parse calendar source to enum
        var calendarSourceEnum = calendarSource switch
        {
            "Fall Winter" => CalendarSource.FallWinter,
            "Summer" => CalendarSource.Summer,
            _ => throw new Exception($"Could not parse calendar source: {calendarSource}")
        };

        var courseHeaders = document.QuerySelectorAll("div.container-fluid.col-md-12 > h4");

        // each course header presents a course offering of a possibly existing course
        // if summer skip the first course header since for some reason there is a random text block which is h4 in summer but h3 in fall/winter lol
        foreach (var courseHeader in IsSummerTerm ? courseHeaders.Skip(1) : courseHeaders)
        {
            var scrapedCourse = ScrapeCourse(courseHeader);
            scrapedCourse.FacultyId = faculty.Id;

            var existingCourse = await courseRepository.GetSingleOrDefaultAsync(x =>
                x.Number == scrapedCourse.Number && x.FacultyId == faculty.Id);
            if (existingCourse == null)
            {
                await courseRepository.InsertAsync(scrapedCourse);
                courses.Add(scrapedCourse);
            }

            var course = existingCourse ?? scrapedCourse;

            var scrapedCourseOffering = new CourseOffering(year, GetSuffix(courseHeader), calendarSourceEnum, course.Id,
                parsedTermId);

            // need to handle the case where the offering is already added, but we are doing the lab, sec, tut split
            var existingCourseOffering = await courseOfferingRepository.GetSingleOrDefaultAsync(x =>
                x.CourseId == course.Id && x.Year == year && x.Suffix == GetSuffix(courseHeader) &&
                x.CalendarSource == calendarSourceEnum && x.TermId == parsedTermId);
            
            var courseOffering = existingCourseOffering ?? scrapedCourseOffering;
            Context.Offering = courseOffering;

            if (existingCourseOffering == null)
            {
                await courseOfferingRepository.InsertAsync(scrapedCourseOffering);
            }

            await ScrapeOfferingSections(courseHeader);
        }

        return courses;
    }

    private static async Task<List<Course>> ProcessFacultyDocuments(List<IDocument> facultyDocuments, Faculty faculty)
    {
        var courses = new List<Course>();
        foreach (var document in facultyDocuments)
            if (DidSearchSucceed(document))
            {
                var facultyCourses = await PopulateCoursesInDocument(document, faculty);
                courses.AddRange(facultyCourses);
            }
            else
            {
                throw new Exception("Search did not succeed");
            }

        return courses;
    }

    public static async Task<List<Course>> PopulateCoursesByFaculty(Faculty faculty)
    {
        var campusTypes = new Dictionary<Campus, KeyValuePair<string, string>>
        {
            { Campus.All, new KeyValuePair<string, string>("Campus", "Any") },
            { Campus.King, new KeyValuePair<string, string>("Campus", "KINGS") },
            { Campus.Huron, new KeyValuePair<string, string>("Campus", "HURON") },
            { Campus.Main, new KeyValuePair<string, string>("Campus", "MAIN") }
        };

        var sectionTypes = new Dictionary<ComponentType, KeyValuePair<string, string>>
        {
            { ComponentType.Lecture, new KeyValuePair<string, string>("course_component", "LEC") },
            { ComponentType.Tutorial, new KeyValuePair<string, string>("course_component", "TUT") },
            { ComponentType.Lab, new KeyValuePair<string, string>("course_component", "LAB") }
        };

        // Try opening the document with all campuses and every single section type 
        var document = await OpenFacultyDocument(faculty,
            sectionTypes.Values.ToList().Append(campusTypes[Campus.All]).ToList());

        try
        {
            return await ProcessFacultyDocuments(new List<IDocument> { document }, faculty);
        }
        catch
        {
            // ignored
        }

        // Try the fine-grained approach and make a query for each campus and section type all 9 combinations (king, huron, main) x (lec, tut, lab)
        var facultyDocuments = new List<IDocument>();
        foreach (var campusType in campusTypes)
        foreach (var sectionType in sectionTypes)
        {
            if (campusType.Key == Campus.All) continue;
            var documentForCampusAndSectionType = await OpenFacultyDocument(faculty,
                new List<KeyValuePair<string, string>> { sectionType.Value, campusType.Value });
            facultyDocuments.Add(documentForCampusAndSectionType);
        }

        return await ProcessFacultyDocuments(facultyDocuments, faculty);
    }
}