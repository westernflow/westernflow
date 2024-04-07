using Data.Entities;
using Repositories.Interfaces;

namespace Scrapers.Utilities;

public static class DbInitializer
{
    public static IServiceProvider? ServiceProvider { get; set; }

    public static async Task ScrapeCurrentTerm(bool isSummer)
    {
        CourseScraper.IsSummerTerm = isSummer;
        var faculties = await PopulateFaculties();
        await PopulateCourses(faculties);
    }

    private static async Task<List<Faculty>> PopulateFaculties()
    {
        var facultyRepository = (ServiceProvider ?? throw new InvalidOperationException())
            .GetRequiredService<IFacultyRepository>();

        var faculties = await CourseScraper.ScrapeFaculties();
        await facultyRepository.InsertRangeAsync(faculties);
        return faculties;
    }

    private static async Task PopulateCourses(List<Faculty> faculties)
    {
        var facultyRepository = (ServiceProvider ?? throw new InvalidOperationException())
            .GetRequiredService<IFacultyRepository>();

        // log progress as percentage of faculties completed
        double progress = 0;
        var increment = 100.0 / faculties.Count;
        foreach (var faculty in faculties)
        {
            Console.WriteLine("Processing faculty: " + faculty.Name);
            await CourseScraper.PopulateCoursesByFaculty(faculty);
            progress += increment;
            Console.WriteLine($"Progress: {progress}% finished scraping courses for {faculty.Name}");
        }
    }
}