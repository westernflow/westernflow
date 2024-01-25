using Data;
using Data.Extensions;
using Repositories.Extensions;

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
        services.AddRepositories();
        services.AddGraphQLServer()
            .AddQueryType<Query>()
            .AddFiltering()
            .ModifyRequestOptions(o => o.IncludeExceptionDetails = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development");
    }

    public void Configure(IApplicationBuilder app)
    {
    }
}
