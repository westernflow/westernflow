using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using Data.Entities;
using Data.Enums;
using Repositories.Interfaces;
using Scrapers.ScrapingUtilities;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace Scrapers.Utilities;

public static class CourseScraper
{
    public static IServiceProvider? ServiceProvider { get; set; } = null;

    public static async Task<List<Faculty>> ScrapeFaculties()
    {
        var configuration = (ServiceProvider ?? throw new InvalidOperationException()).GetRequiredService<IConfiguration>();
        // Get a new cookie
        var cookie = await CookieManager.GetCookie(configuration);
        
        // create new http client for the request to pass in headers
        var client = new HttpClient();
        client.DefaultRequestHeaders.Add("Cookie", cookie.Value);
        
        var response = await client.GetAsync(configuration["Scraper:BuilderUrl"]);
        var html = await response.Content.ReadAsStringAsync();
        
        if (string.IsNullOrWhiteSpace(html))
        {
            throw new Exception("No html was returned from the request");
        }
        
        // parse the HTML string to DOM nodes for AngleSharp
        var config = Configuration.Default;
        var context = BrowsingContext.New(config);
        var document = await context.OpenAsync(req => req.Content(html));

        if (document == null)
        {
            throw new Exception("No document was returned from the request");
        }

        var faculties = new List<Faculty>();
        var options = document.QuerySelectorAll("select#Subject > option");

        foreach (var option in options)
        {
            var abbreviation = option.GetAttribute("value");
            var name = option.TextContent.Trim();

            // Skip the first option as it is just a placeholder
            if (!string.IsNullOrWhiteSpace(abbreviation))
            {
                faculties.Add(new Faculty(name, abbreviation));
            }
        }

        return faculties;
    }

    public static async Task<IDocument> OpenFacultyDocument(Faculty faculty)
    {
        var configuration = (ServiceProvider ?? throw new InvalidOperationException()).GetRequiredService<IConfiguration>();
        
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
            new KeyValuePair<string, string>("Campus", "Any"),
            new KeyValuePair<string, string>("course_component", "LEC"),
            new KeyValuePair<string, string>("course_component", "TUT"),
            new KeyValuePair<string, string>("course_component", "LAB"),
            new KeyValuePair<string, string>("command_search", "search")
        });

        var response = await client.PostAsync(configuration["Scraper:BuilderUrl"], requestData);
        var html = await response.Content.ReadAsStringAsync();

        if (string.IsNullOrWhiteSpace(html))
        {
            throw new Exception("No html was returned from the request");
        }

        // parse the HTML string to DOM nodes for AngleSharp
        var config = Configuration.Default;
        var context = BrowsingContext.New(config);
        var document = await context.OpenAsync(req => req.Content(html));
        
        if (document == null)
        {
            throw new Exception("No document was returned from the request");
        }

        return document;
    }

    public static Course ScrapeCourse(IElement courseHeader)
    {
        var descriptionElement = courseHeader.NextElementSibling;
        if (descriptionElement == null)
        {
            throw new Exception("Could not find description element");
        }

        var description = descriptionElement.TextContent.Trim();

        var courseName = courseHeader.TextContent.Split(" - ")[1].Trim();
        var courseNumber = courseHeader.TextContent.Split(" - ")[0].Split(" ")[1].Trim();

        // if last character is a letter, remove it
        if (char.IsLetter(courseNumber[^1]))
        {
            courseNumber = courseNumber.Substring(0, courseNumber.Length - 1);
        }

        if (!int.TryParse(courseNumber, out var parsedCourseNumber))
        {
            throw new Exception("Could not parse course number");
        }

        // get internal course id
        var htmlElement = descriptionElement.NextElementSibling;
        while (htmlElement != null && htmlElement.TagName != "TABLE")
        {
            htmlElement = htmlElement.NextElementSibling;
        }
        var tableElement = htmlElement as IHtmlTableElement;
        if (tableElement == null)
        {
            throw new Exception("Could not find table element");
        }

        var inputElement = tableElement.QuerySelector("input[name=crse_id]");
        if (inputElement == null)
        {
            Console.WriteLine(courseName);
            throw new Exception("Could not find input element");
        }

        // set internal course id to the value of the input element
        var internalCourseId = inputElement.GetAttribute("value");
        if (string.IsNullOrWhiteSpace(internalCourseId))
        {
            throw new Exception("Could not find internal course id");
        }

        var courseConstructorParams = new CourseConstructorParams
        {
            Number = parsedCourseNumber,
            Name = courseName,
            Description = description,
            BreadthCategory = BreadthCategory.Undefined,
            InternalCourseId = internalCourseId
        };

        var course = new Course(courseConstructorParams);

        return course;
    }

    public static Suffix GetSuffix(IElement courseHeader)
    {
        var courseNumber = courseHeader.TextContent.Split(" - ")[0].Split(" ")[1];
        
        // suffix is either at end of course number or not present
        // check if last character is a letter
        if (!char.IsLetter(courseNumber[^1])) return Suffix.NoSuffix;
        
        var suffixChar = courseNumber[^1];
        // try to convert to suffix enum
        if (Enum.TryParse<Suffix>(suffixChar.ToString(), out var suffix))
        {
            return suffix;
        }
            
        throw new Exception("Could not parse suffix");

    }

    public static TimingDetails ScrapeTimingDetail(IHtmlTableRowElement row)
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
        var daysOfWeekToBitmapIndex = new Dictionary<DayOfWeek, int>()
        {
            {DayOfWeek.Monday, 0},
            {DayOfWeek.Tuesday, 1},
            {DayOfWeek.Wednesday, 2},
            {DayOfWeek.Thursday, 3},
            {DayOfWeek.Friday, 4},
            {DayOfWeek.Saturday, 5},
            {DayOfWeek.Sunday, 6}
        };
        
        // convert days of week to bitmap
        var daysOfWeekBits = new List<char> {'0', '0', '0', '0', '0', '0', '0'};
        foreach (var dayOfWeek in daysOfWeek)
        {
            daysOfWeekBits[daysOfWeekToBitmapIndex[dayOfWeek]] = '1';
        }
        var daysOfWeekBitmap = new string(daysOfWeekBits.ToArray());
        
        // 2. get time
        var time = cells[1].TextContent.Trim() ?? string.Empty;
        if (time == "-")
        {
            time = string.Empty;
        }
        
        return new TimingDetails()
        {
            DaysOfWeekBitmap = daysOfWeekBitmap,
            Time = time,
            Location = cells[2].TextContent.Trim()
        };
    }
    
    public static async Task<Section> ScrapeSection(IHtmlTableRowElement row, int courseOfferingId)
    {
        var sectionRepository = (ServiceProvider ?? throw new InvalidOperationException()).GetRequiredService<ISectionRepository>();
        
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
        {
            throw new Exception($"Could not parse section number: {sectionNumberString}");
        }
        
        // 3. get class number
        var classNumberString = cells[2].TextContent.Trim();
        if (!int.TryParse(classNumberString, out var classNumber))
        {
            throw new Exception($"Could not parse class number: {classNumberString}");
        }
        
        // 4. get instructors (they are separated by <br> tags so split based on the html)
        var instructorNames = cells[3].InnerHtml.Split("<br>").Select(x => x.Trim()).ToList();
        
        // there is a trailing <br> tag so remove the last element
        instructorNames.RemoveAt(instructorNames.Count - 1);

        // 5. get timetable requisite string
        var timetableRequisiteString = cells[4].TextContent.Trim();
        
        // 6. Section location and times
        var sectionLocationAndTimesTable = cells[5].QuerySelector("table") as IHtmlTableElement;
        if (sectionLocationAndTimesTable == null)
        {
            throw new Exception("Could not find section location and times table");
        }
        
        var timingDetails = new List<TimingDetails>();
        foreach (var sectionLocationAndTimesRow in sectionLocationAndTimesTable.Rows)
        {
            var timingDetail = ScrapeTimingDetail(sectionLocationAndTimesRow);
            timingDetails.Add(timingDetail);
        }
        
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
        {
            throw new Exception($"Could not parse waitList size: {waitListSizeString}");
        }
        
        // 9. get campus
        var campusString = cells[9].TextContent.Trim();
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
        var deliveryTypeString = cells[10].TextContent.Trim();
        var deliveryType = deliveryTypeString switch
        {
            "In Person" => DeliveryType.InPerson,
            "Distance Studies/Online" => DeliveryType.Online,
            "Blended" => DeliveryType.Hybrid,
            "Other" => DeliveryType.Other,
            _ => throw new Exception($"Could not parse delivery type: {deliveryTypeString}")
        };
        
        var sectionParams = new SectionConstructorParams()
        {   
            ComponentType = component,
            Number = sectionNumber,
            ClassNumber = classNumber,
            ProfessorNames = instructorNames,
            TimetableRequisiteString = timetableRequisiteString,
            TimingDetails = timingDetails,
            Status = status,
            WaitListSize = waitListSize,
            Campus = campus,
            Delivery = deliveryType,
            CourseOfferingId = courseOfferingId
        };
        
        var section = new Section(sectionParams);
        
        // insert to the database
        await sectionRepository.InsertAsync(section);
        return section;
    }

    public static async Task<List<Section>> ScrapeAndPopulateOfferingSections(IElement courseHeader, int courseOfferingId)
    {
        var sections = new List<Section>();
        
        var htmlElement = courseHeader.NextElementSibling;
        while (htmlElement != null && htmlElement.TagName != "TABLE")
        {
            htmlElement = htmlElement.NextElementSibling;
        }
        var tableElement = htmlElement as IHtmlTableElement;
        if (tableElement == null)
        {
            throw new Exception("Could not find table element");
        }

        foreach (var row in tableElement.Rows.Skip(1))
        {
            var section = await ScrapeSection(row, courseOfferingId);
            sections.Add(section);
        }

        return sections;
    }

    public static bool DidSearchSucceed(IDocument page)
    {
        // <div class="alert alert-warning" role="alert"> Unable to display your search results as it exceeds 300 courses. Please refine your search. </div>
        var alertElement = page.QuerySelector("div.alert.alert-warning");
        if (alertElement == null)
        {
            return true;
        }
        
        if (alertElement.InnerHtml.Contains("Unable to display your search results as it exceeds 300 courses. Please refine your search."))
        {
            return false;
        }

        return true;
    }
    
    public static async Task<List<Course>> PopulateCoursesByFaculty(Faculty faculty)
    {
        var configuration = (ServiceProvider ?? throw new InvalidOperationException()).GetRequiredService<IConfiguration>();
        var document = await OpenFacultyDocument(faculty);
        
        var courses = new List<Course>();
        
        var courseHeaders = document.QuerySelectorAll("div.container-fluid.col-md-12 > h4");

        // each course header presents a course offering of a possibly existing course
        foreach (var courseHeader in courseHeaders)
        {
            var scrapedCourse = ScrapeCourse(courseHeader);
            // prior to scraping the course, check if it already exists in the list
            var existingCourse = courses.FirstOrDefault(x => x.Number == scrapedCourse.Number && x.FacultyId == faculty.Id);
            
            if (existingCourse == null)
            {
                scrapedCourse.FacultyId = faculty.Id;
                
                // insert the course into the database
                var courseRepository = ServiceProvider.GetRequiredService<ICourseRepository>();
                await courseRepository.InsertAsync(scrapedCourse);
                
                courses.Add(scrapedCourse);
            }
            var course = existingCourse ?? scrapedCourse;
            
            // create a new course offering
            var courseOfferingRepository = ServiceProvider.GetRequiredService<ICourseOfferingRepository>();
            var courseOffering = new CourseOffering(2024, GetSuffix(courseHeader), course.Id);
            course.CourseOfferings.Add(courseOffering);
            
            // insert the course offering into the database
            await courseOfferingRepository.InsertAsync(courseOffering);

            // populate the course offering with the sessions 
            var sections = await ScrapeAndPopulateOfferingSections(courseHeader, courseOffering.Id);
            courseOffering.Sections = sections;
        }
        
        return courses;
    } 
}