using System.Security.Claims;
using Data;
using Data.Entities;
using Data.Extensions;
using graphql.DataLoaders;
using graphql.Types;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Repositories.Extensions;

namespace graphql;

public class Startup
{
    private IConfiguration _configuration { get; }

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.Authority = _configuration["Auth0:Authority"];
            options.Audience = _configuration["Auth0:Audience"];
            options.IncludeErrorDetails = true;
            // Log detailed errors
            options.Events = new JwtBearerEvents
            {
                OnAuthenticationFailed = context =>
                {
                    Console.WriteLine($"Authentication failed: {context.Exception.Message}");
                    return Task.CompletedTask;
                },
                OnChallenge = context =>
                {
                    Console.WriteLine(context.Request.Body);
                    if (context.AuthenticateFailure != null)
                    {
                        Console.WriteLine($"Authentication challenge failed: {context.AuthenticateFailure.Message}");
                    }
                    else
                    {
                        Console.WriteLine("Authentication challenge failed.");
                        Console.WriteLine($"error description... {context.ErrorDescription}");
                    }
                    return Task.CompletedTask;
                },
            };
        }); 
        services.AddCors();
        services.AddCourseManagerDbContext(_configuration);
        services.AddScopedRepositories();
        services.AddControllers();
        services.AddGraphQLServer()
            .AddQueryType<Query>()
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
        var origins = _configuration["AllowedOrigins"]?.Split(",") ?? new string[] { "http://localhost:3000" };
        foreach (var origin in origins)
        {
            Console.WriteLine($"Allowed origin: {origin}");
        }

        app.UseCors(
            options => options.WithOrigins(origins).WithMethods("GET", "POST", "OPTIONS").AllowAnyHeader()
        );
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
    }
}