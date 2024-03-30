using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Data.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddCourseManagerDbContext(this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("CourseManagerDbConnection");
        serviceCollection.AddPooledDbContextFactory<CourseManagerDbContext>(
            builder => builder.UseNpgsql(connectionString));
        return serviceCollection;
    }
}