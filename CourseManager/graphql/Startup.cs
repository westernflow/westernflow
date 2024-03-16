using Business.Interfaces;
using Business.Providers;
using Data.Extensions;
using graphql.DataLoaders;
using graphql.Types;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Repositories.Extensions;

namespace graphql;

public class Startup
{
    private IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.Authority = Configuration["Auth0:Authority"];
            options.Audience = Configuration["Auth0:Audience"];
            options.IncludeErrorDetails = true;
        });
        services.AddCors();
        services.AddCourseManagerDbContext(Configuration);
        services.AddScopedRepositories();
        services.AddScoped<IReviewerInfoProvider, ReviewerInfoProvider>();
        services.AddHttpContextAccessor();
        services.AddGraphQLServer()
            .AddQueryType<Query>()
            .AddMutationType<Mutation>()
            .AddMutationConventions()
            .AddType<CourseType>()
            .AddType<CourseOfferingType>()
            .AddType<SectionType>()
            .AddGlobalObjectIdentification()
            .AddDataLoader<CourseBatchDataLoader>()
            .AddDataLoader<FacultyBatchDataLoader>()
            .AddDataLoader<CourseOfferingGroupedDataLoader>()
            .AddDataLoader<SectionGroupedDataLoader>()
            .AddDataLoader<TimingDetailsGroupedDataLoader>()
            .AddFiltering()
            .AddProjections()
            .ModifyRequestOptions(o =>
                o.IncludeExceptionDetails = true);
    }

    public void Configure(IApplicationBuilder app)
    {
        var origins = Configuration["AllowedOrigins"]?.Split(",") ?? new string[] { "http://localhost:3000" };
        foreach (var origin in origins)
        {
            Console.WriteLine($"Allowed origin: {origin}");
        }

        app.UseCors(
            options => options.WithOrigins(origins).WithMethods("GET", "POST", "OPTIONS").AllowAnyHeader()
        );
        app.UseRouting();
        app.UseAuthentication();
    }
}