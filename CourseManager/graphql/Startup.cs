using Data;
using Data.Entities;
using Data.Extensions;
using graphql.DataLoaders;
using graphql.Types;
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
        services.AddScopedRepositories();
        services.AddGraphQLServer()
            .AddQueryType<Query>()
            .AddType<CourseType>()
            .AddType<CourseOfferingType>()
            .AddType<SectionType>()
            .AddDataLoader<CourseBatchDataLoader>()
            .AddDataLoader<FacultyBatchDataLoader>()
            .AddDataLoader<CourseOfferingGroupedDataLoader>()
            .AddDataLoader<SectionGroupedDataLoader>()
            .AddDataLoader<TimingDetailsGroupedDataLoader>()
            .AddFiltering()
            .AddProjections()
            .ModifyRequestOptions(o => o.IncludeExceptionDetails = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development");
    }

    public void Configure(IApplicationBuilder app)
    {
    }
}
