using Data;
using Data.Extensions;

namespace graphql;

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
        services.AddGraphQLServer()
            .AddTypes();
    }

    public void Configure(IApplicationBuilder app)
    {
    }
}
