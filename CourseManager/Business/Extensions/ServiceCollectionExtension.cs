using Business.Interfaces;
using Business.Providers;
using Business.Services;
using Business.Validators;
using Microsoft.Extensions.DependencyInjection;

namespace Business.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddScopedBusinessProviders(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IReviewerInfoProvider, ReviewerInfoProvider>();
        return serviceCollection;
    }
    
    public static IServiceCollection AddScopedBusinessServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<ICourseReviewService, CourseReviewService>();
        return serviceCollection;
    }
    
    public static IServiceCollection AddScopedBusinessValidators(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<CreateCourseReviewInputValidator>();
        return serviceCollection;
    }
}