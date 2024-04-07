namespace Scrapers;

public class Program
{
    public static void Main(string[] args)
    {
        var host = Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                var startup = new Startup(hostContext.Configuration);
                startup.ConfigureServices(services);
            })
            .ConfigureLogging(logger =>
                logger.AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Warning))
            .Build();
        host.Run();
    }
}