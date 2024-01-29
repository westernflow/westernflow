using Scrapers.ScrapingUtilities;

namespace Scrapers;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IHostApplicationLifetime _appLifetime;
    private readonly IConfiguration _configuration;

    public Worker(ILogger<Worker> logger, IHostApplicationLifetime appLifetime, IConfiguration configuration)
    {
        _logger = logger;
        _appLifetime = appLifetime;
        _configuration = configuration;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Worker started running at: {time}", DateTimeOffset.Now);

        // get cookies
        var cookie = await CookieManager.GetCookie(_configuration);
        Console.WriteLine(cookie);
        
        // await CourseScraper.Scrape(); 
        
        // ExecuteAsync runs for the entire lifetime of the Application. We want to run the 
        // ExecuteAsync code then kill the application since there is no more work left to do.
        _appLifetime.StopApplication();
    }
}