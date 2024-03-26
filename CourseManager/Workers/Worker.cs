using Repositories.Interfaces;
using Scrapers.ScrapingUtilities;
using Scrapers.Utilities;

namespace Scrapers;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IHostApplicationLifetime _appLifetime;
    private readonly IServiceProvider _serviceProvider;

    public Worker(
        ILogger<Worker> logger, 
        IHostApplicationLifetime appLifetime, 
        IServiceProvider serviceProvider)
    {
        _logger = logger;
        _appLifetime = appLifetime;
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Worker started running at: {time}", DateTimeOffset.Now);
        
        // anti-pattern since this makes testing harder but I am not testing either way l0l
        DbInitializer.ServiceProvider = _serviceProvider;
        CourseScraper.ServiceProvider = _serviceProvider;

        await DbInitializer.ScrapeCurrentTerm(isSummer: false);
        
        // ExecuteAsync runs for the entire lifetime of the Application. We want to run the 
        // ExecuteAsync code then kill the application since there is no more work left to do.
        _appLifetime.StopApplication();
    }
}