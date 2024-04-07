using System.Text.RegularExpressions;
using AngleSharp;
using Data.Entities;
using Data.Enums;
using Repositories.Interfaces;

namespace Scrapers.Utilities;

public static class PublicScraper
{
    public static IServiceProvider? ServiceProvider { get; set; }

    private const string BaseUrl = "https://westerncalendar.uwo.ca/";
    private const string FacultyUrl = "Courses.cfm?SelectedCalendar=Live&ArchiveID=";

    private class Paragraph
    {
        internal enum ParagraphType
        {
            Description,
            Antirequisites,
            Prerequisites,
            PreCorequisites,
            ExtraInformation,
            CourseWeight,
        }

        public ParagraphType Type { get; set; }

        public string Content { get; set; } = string.Empty;

        public override string ToString()
        {
            return $"{Type}: {Content}";
        }

        public class PrefixConstants
        {
            public const string Antirequisites = "Antirequisite(s): ";
            public const string ExtraInformation = "Extra Information: ";
            public const string CourseWeight = "Course Weight: ";
            public const string Prerequisites = "Prerequisite(s): ";
            public const string PreCorequisites = "Pre-or Corequisite(s): ";
            public const string MoreDetails = "More details";
        }
    }

    private static List<Paragraph> ScrapeParagraph(string divContent)
    {
        divContent = divContent.Trim();
        var paragraphs = new List<Paragraph>();

        if (divContent.StartsWith(Paragraph.PrefixConstants.Antirequisites))
        {
            paragraphs.Add(new Paragraph
            {
                Type = Paragraph.ParagraphType.Antirequisites,
                Content = divContent.Substring(Paragraph.PrefixConstants.Antirequisites.Length),
            });
        }
        else if (divContent.StartsWith(Paragraph.PrefixConstants.ExtraInformation))
        {
            paragraphs.Add(new Paragraph
            {
                Type = Paragraph.ParagraphType.ExtraInformation,
                Content = divContent.Substring(Paragraph.PrefixConstants.ExtraInformation.Length),
            });
        }
        else if (divContent.StartsWith(Paragraph.PrefixConstants.CourseWeight))
        {
            divContent = divContent.Split(Paragraph.PrefixConstants.MoreDetails)[0].Trim();
            paragraphs.Add(new Paragraph
            {
                Type = Paragraph.ParagraphType.CourseWeight,
                Content = divContent.Substring(Paragraph.PrefixConstants.CourseWeight.Length),
            });
        }
        else if (divContent.Contains(Paragraph.PrefixConstants.Prerequisites) &&
                 divContent.Contains(Paragraph.PrefixConstants.PreCorequisites))
        {
            var prereqParagraph = divContent.Split(new string[] { Paragraph.PrefixConstants.PreCorequisites },
                StringSplitOptions.None)[0];
            var processedPreAndCoreqParagraph = divContent.Split(
                new string[] { Paragraph.PrefixConstants.PreCorequisites },
                StringSplitOptions.None)[1];

            paragraphs.Add(new Paragraph
            {
                Type = Paragraph.ParagraphType.Prerequisites,
                Content = prereqParagraph.Substring(Paragraph.PrefixConstants.Prerequisites.Length),
            });

            paragraphs.Add(new Paragraph
            {
                Type = Paragraph.ParagraphType.PreCorequisites,
                Content = processedPreAndCoreqParagraph
            });
        }
        else if (divContent.StartsWith(Paragraph.PrefixConstants.Prerequisites))
        {
            paragraphs.Add(new Paragraph
            {
                Type = Paragraph.ParagraphType.Prerequisites,
                Content = divContent.Substring(Paragraph.PrefixConstants.Prerequisites.Length),
            });
        }
        else
        {
        }

        return paragraphs;
    }

    public static async Task ScrapePublicCourseInformation()
    {
        var facultyRepository = (ServiceProvider ?? throw new InvalidOperationException())
            .GetRequiredService<IFacultyRepository>();
        var courseRepository = ServiceProvider.GetRequiredService<ICourseRepository>();
        // get request on following link to get the html https://westerncalendar.uwo.ca/Courses.cfm?SelectedCalendar=Live&ArchiveID=
        var client = new HttpClient();
        var response = await client.GetAsync(BaseUrl + FacultyUrl);
        var content = await response.Content.ReadAsStringAsync();
        var document = BrowsingContext.New(Configuration.Default).OpenAsync(req => req.Content(content)).Result;

        var tbody = document.QuerySelector("tbody");
        if (tbody == null)
        {
            throw new InvalidOperationException("Could not find tbody element in the document");
        }

        var rows = tbody.QuerySelectorAll("tr");

        foreach (var row in rows)
        {
            var cells = row.QuerySelectorAll("td");
            // example cells[0] html: <a href="Courses.cfm?Subject=ACTURSCI&amp;SelectedCalendar=Live&amp;ArchiveID=">Actuarial Science</a>
            // get the href attribute value and get the subject value
            var name = cells[0].QuerySelector("a")?.TextContent;
            var abbreviation = cells[0].QuerySelector("a")?.GetAttribute("href")?.Split("Subject=")[1].Split("&")[0];

            if (name == null || abbreviation == null)
            {
                throw new InvalidOperationException("Could not find faculty name or abbreviation");
            }

            // check faculty repository for existing faculty with the same abbreviation or name
            var existingFaculty =
                await facultyRepository.GetSingleOrDefaultAsync(f => f.Abbreviation == abbreviation);

            // Get the breadth category string in cells[1]
            var breadthCategory = cells[1].TextContent;

            // e.g. CATEGORY A CATEGORY B => ["CATEGORY", "A", "CATEGORY", "B"] => [e.undefined, e.a, e.undefined, e.b] => 110
            var categories = breadthCategory.Split(" ");
            var breadthCategories = categories.Select(BreadthCategoryUtils.GetBreadthCategory).ToList();
            var bitmap = BreadthCategoryUtils.CreateBreadthCategoryBitmap(breadthCategories);

            var currentFaculty = existingFaculty ?? new Faculty()
            {
                Name = name,
                Abbreviation = abbreviation,
                EnumBitmap = bitmap
            };
            
            currentFaculty.EnumBitmap = bitmap;
            await facultyRepository.UpdateAsync(currentFaculty);
            Console.WriteLine(currentFaculty.Abbreviation);
            
            // get the value of the href attribute value and append it to the base url and make a get request
            // to get the html of the page
            var link = cells[0].QuerySelector("a")?.GetAttribute("href");
            if (link == null)
            {
                throw new InvalidOperationException("Could not find link to faculty page");
            }

            var facultyResponse = await client.GetAsync(BaseUrl + link);
            var facultyContent = await facultyResponse.Content.ReadAsStringAsync();

            var facultyDocument = BrowsingContext.New(Configuration.Default)
                .OpenAsync(req => req.Content(facultyContent)).Result;
            var courseRows = facultyDocument.QuerySelectorAll("div.col-md-12").Skip(1);

            foreach (var courseRow in courseRows)
            {
                // courseHeaderText is stored in an h4 element with class courseTitleNoBlueLink
                var courseHeaderText = courseRow.QuerySelector("h4")?.TextContent;
                var image = courseRow.QuerySelector("img")?.GetAttribute("src")?.Trim();
                if (image == null || image != "images/westernStacked.png")
                {
                    continue;
                }
                
                // get courseHeaderText = facultyName + " " + courseNumber + prefixes.join("/") + "  " + courseName
                // e.g. "Actuarial Science 4824A/B SHORT TERM ACTUARIAL MATHEMATICS II"
                if (courseHeaderText == null)
                {
                    throw new InvalidOperationException("Could not find course header text");
                }

                // check if the string starts with the faculty name and throw an exception if it doesn't
                if (!courseHeaderText.StartsWith(name))
                {
                    throw new InvalidOperationException("Course header text does not start with faculty name");
                }

                // remove length of faculty name + 1 (for the space) from the start of the string
                var courseHeaderTextWithoutFacultyName = courseHeaderText.Substring(name.Length + 1);
                var courseNumberAndPrefixes = courseHeaderTextWithoutFacultyName.Split(" ")[0];
                // sometimes the course number is in a range e.g. Law 5200-5999Law Supplemental Writing Credit  
                // filter for this case where after removing facultyname 5200-5999Law is courseNumberAndPrefixes
                if (courseNumberAndPrefixes.Contains("-"))
                {
                    continue;
                }
                var courseNumber = int.Parse(Regex.Match(courseNumberAndPrefixes, @"\d+").Value);
                var courseName = courseHeaderTextWithoutFacultyName.Substring(courseNumberAndPrefixes.Length + 1).Trim();
                // Console.WriteLine($"Scraping course: {currentFaculty.Name} {courseNumber} - {courseName}");

                var paragraphs = new List<Paragraph>();

                // select all the div class=col-xs-12 in the courseElement to get relevant paragraphs
                var courseParagraphs = courseRow.QuerySelectorAll("div.col-xs-12");
                if (courseParagraphs == null)
                {
                    throw new InvalidOperationException("Could not find course paragraphs");
                }
                
                // try to scrape the first paragraph -- if error it is highly likely it is just the description
                var firstParagraphs = ScrapeParagraph(courseParagraphs.First().TextContent);
                paragraphs.AddRange(firstParagraphs);
                
                if (firstParagraphs.Count == 0)
                {
                    paragraphs.Add(new Paragraph
                    {
                        Type = Paragraph.ParagraphType.Description,
                        Content = courseParagraphs.First().TextContent.Trim(),
                    });
                }
                
                foreach (var courseParagraph in courseParagraphs.Skip(1))
                {
                    var paragraph = ScrapeParagraph(courseParagraph.TextContent);
                    paragraphs.AddRange(paragraph);
                }

                var weightString = paragraphs.FirstOrDefault(p => p.Type == Paragraph.ParagraphType.CourseWeight)
                    ?.Content;
                decimal? weight = null;
                if (!string.IsNullOrWhiteSpace(weightString))
                {
                    weight = decimal.Parse(weightString ?? throw new InvalidOperationException());
                }
                
                await courseRepository.UpsertAsync(new Course
                {
                    Number = courseNumber,
                    Name = courseName,
                    FacultyId = currentFaculty.Id,
                    Description =
                        paragraphs.FirstOrDefault(p => p.Type == Paragraph.ParagraphType.Description)?.Content ??
                        string.Empty,
                    Prerequisites =
                        paragraphs.FirstOrDefault(p => p.Type == Paragraph.ParagraphType.Prerequisites)?.Content ??
                        string.Empty,
                    Corequisites =
                        paragraphs.FirstOrDefault(p => p.Type == Paragraph.ParagraphType.PreCorequisites)?.Content ??
                        string.Empty,
                    Antirequisites =
                        paragraphs.FirstOrDefault(p => p.Type == Paragraph.ParagraphType.Antirequisites)?.Content ??
                        string.Empty,
                    ExtraInformation =
                        paragraphs.FirstOrDefault(p => p.Type == Paragraph.ParagraphType.ExtraInformation)?.Content ??
                        string.Empty,
                    Weight = weight
                },
                x => x.Description,
                x => x.Prerequisites,
                x => x.Corequisites,
                x => x.Antirequisites,
                x => x.ExtraInformation,
                x => x.Weight!);
                
                // Console.WriteLine($"Scraped course: {courseName}");
            }
        }
    }
}