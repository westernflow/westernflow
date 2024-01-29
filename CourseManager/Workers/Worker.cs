using Repositories.Interfaces;
using Scrapers.ScrapingUtilities;

namespace Scrapers;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IHostApplicationLifetime _appLifetime;
    private readonly IConfiguration _configuration;
    private readonly IFacultyRepository _facultyRepository;

    public Worker(ILogger<Worker> logger, IHostApplicationLifetime appLifetime, IConfiguration configuration, IFacultyRepository facultyRepository)
    {
        _logger = logger;
        _appLifetime = appLifetime;
        _configuration = configuration;
        _facultyRepository = facultyRepository;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Worker started running at: {time}", DateTimeOffset.Now);

        await CourseScraper.GetFaculties(_configuration, _facultyRepository);
        
        // ExecuteAsync runs for the entire lifetime of the Application. We want to run the 
        // ExecuteAsync code then kill the application since there is no more work left to do.
        _appLifetime.StopApplication();
    }
}