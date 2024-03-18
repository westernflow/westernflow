using Data.Entities;
using Repositories.Interfaces;
using Scrapers.ScrapingUtilities;

namespace Scrapers.Utilities;

public static class DbInitializer
{
    public static IServiceProvider? ServiceProvider { get; set; }

    public static async Task InitializeDatabase()
    {
        await PopulateFaculties();
        await PopulateCourses();
    }

    private static async Task PopulateFaculties()
    {
        var facultyRepository = (ServiceProvider ?? throw new InvalidOperationException()).GetRequiredService<IFacultyRepository>();

        var faculties = await CourseScraper.ScrapeFaculties();
        await facultyRepository.InsertRangeAsync(faculties);
    }

    private static async Task PopulateCourses()
    {
        var facultyRepository = (ServiceProvider ?? throw new InvalidOperationException()).GetRequiredService<IFacultyRepository>();
        var faculties = await facultyRepository.GetAllAsync();

        // log progress as percentage of faculties completed
        double progress = 0;
        double increment = 100.0 / faculties.Count;
        foreach (var faculty in faculties)
        {
            Console.WriteLine("Processing faculty: " + faculty.Name);
            await CourseScraper.PopulateCoursesByFaculty(faculty);
            progress += increment;
            Console.WriteLine($"Progress: {progress}% finished scraping courses for {faculty.Name}");
        }
    }
}
