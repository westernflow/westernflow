using Repositories.Interfaces;
using Scrapers.ScrapingUtilities;
using Scrapers.Utilities;

namespace Scrapers;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IHostApplicationLifetime _appLifetime;
    private readonly IConfiguration _configuration;
    private readonly IFacultyRepository _facultyRepository;
    private readonly ICourseRepository _courseRepository;
    private readonly ICourseOfferingRepository _courseOfferingRepository;
    private readonly ISectionRepository _sectionRepository;
    private readonly ITimingDetailsRepository _timingDetailRepository;

    public Worker(
        ILogger<Worker> logger, 
        IHostApplicationLifetime appLifetime, 
        IConfiguration configuration, 
        IFacultyRepository facultyRepository, 
        ICourseRepository courseRepository,
        ICourseOfferingRepository courseOfferingRepository,
        ISectionRepository sectionRepository,
        ITimingDetailsRepository timingDetailRepository)
    {
        _logger = logger;
        _appLifetime = appLifetime;
        _configuration = configuration;
        _facultyRepository = facultyRepository;
        _courseRepository = courseRepository;
        _courseOfferingRepository = courseOfferingRepository;
        _sectionRepository = sectionRepository;
        _timingDetailRepository = timingDetailRepository;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Worker started running at: {time}", DateTimeOffset.Now);

        await DbInitializer.InitializeDatabase(_configuration, _facultyRepository, _courseRepository, _courseOfferingRepository, _sectionRepository, _timingDetailRepository);
        
        // ExecuteAsync runs for the entire lifetime of the Application. We want to run the 
        // ExecuteAsync code then kill the application since there is no more work left to do.
        _appLifetime.StopApplication();
    }
}