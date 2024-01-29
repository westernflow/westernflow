using Data.Extensions;
using Repositories.Extensions;

namespace Scrapers;

public class Startup
{
    public IConfiguration Configuration { get; }
    
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }
    
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddCourseManagerDbContext(Configuration);
        services.AddHostedService<Worker>();
        services.AddSingletonRepositories();
    }

    public void Configure(IHost host)
    {
    } 
}