using System.Collections.Immutable;
using Repositories.Interfaces;
using Repositories.Repositories;

namespace Scrapers;

public class Program
{
    public static void Main(string[] args)
    {
        IHost host = Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                var startup = new Startup(hostContext.Configuration);
                startup.ConfigureServices(services);
            })
            .ConfigureLogging(logger => logger.AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Warning))
            .Build();
        host.Run();
    }
}