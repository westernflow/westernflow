using Data.Entities;
using Repositories.Interfaces;
using Scrapers.ScrapingUtilities;

namespace Scrapers.Utilities;

public static class DbInitializer
{
    public static async Task InitializeDatabase(
        IConfiguration configuration, 
        IFacultyRepository facultyRepository, 
        ICourseRepository courseRepository,
        ICourseOfferingRepository courseOfferingRepository,
        ISectionRepository sectionRepository,
        ITimingDetailsRepository timingDetailRepository)
    {
        await PopulateFaculties(configuration, facultyRepository);
        await PopulateCourses(configuration, facultyRepository, courseRepository, courseOfferingRepository, sectionRepository, timingDetailRepository);
    }
    
    public static async Task PopulateFaculties(IConfiguration configuration, IFacultyRepository facultyRepository)
    {
        var faculties = await CourseScraper.ScrapeFaculties(configuration);
        await facultyRepository.InsertRangeAsync(faculties);
    }
    
    public static async Task PopulateCourses(
        IConfiguration configuration, 
        IFacultyRepository facultyRepository, 
        ICourseRepository courseRepository,
        ICourseOfferingRepository courseOfferingRepository,
        ISectionRepository sectionRepository,
        ITimingDetailsRepository timingDetailRepository)
    {
        var faculties = await facultyRepository.GetAllAsync();
        
        // log progress as percentage of faculties completed
        double progress = 0;
        double increment = 100.0 / faculties.Count;
        foreach (var faculty in faculties)
        {
            Console.WriteLine("Processing faculty: " + faculty.Name);
            await CourseScraper.PopulateCoursesByFaculty(configuration, courseRepository, courseOfferingRepository,
                sectionRepository, timingDetailRepository, faculty, 2024);
            progress += increment;
            Console.WriteLine($"Progress: {progress}% finished scraping courses for {faculty.Name}");
        }
    }
}