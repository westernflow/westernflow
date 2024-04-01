using Microsoft.Extensions.DependencyInjection;
using Repositories.Interfaces;
using Repositories.Interfaces.JoinTables;
using Repositories.Repositories;
using Repositories.Repositories.JoinTables;

namespace Repositories.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddScopedRepositories(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<ICourseRepository, CourseRepository>();
        serviceCollection.AddScoped<IFacultyRepository, FacultyRepository>();
        serviceCollection.AddScoped<IProfessorRepository, ProfessorRepository>();
        serviceCollection.AddScoped<ISectionRepository, SectionRepository>();
        serviceCollection.AddScoped<ITimingDetailsRepository, TimingDetailsRepository>();
        serviceCollection.AddScoped<ICourseOfferingRepository, CourseOfferingRepository>();
        serviceCollection.AddScoped<IReviewerRepository, ReviewerRepository>();
        serviceCollection.AddScoped<ICourseReviewRepository, CourseReviewRepository>();
        serviceCollection.AddScoped<ISectionProfessorRepository, SectionProfessorRepository>();
        return serviceCollection;
    }

    public static IServiceCollection AddSingletonRepositories(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<ICourseRepository, CourseRepository>();
        serviceCollection.AddSingleton<IFacultyRepository, FacultyRepository>();
        serviceCollection.AddSingleton<IProfessorRepository, ProfessorRepository>();
        serviceCollection.AddSingleton<ISectionRepository, SectionRepository>();
        serviceCollection.AddSingleton<ITimingDetailsRepository, TimingDetailsRepository>();
        serviceCollection.AddSingleton<ICourseOfferingRepository, CourseOfferingRepository>();
        serviceCollection.AddSingleton<IReviewerRepository, ReviewerRepository>();
        serviceCollection.AddSingleton<ICourseReviewRepository, CourseReviewRepository>();
        serviceCollection.AddSingleton<ISectionProfessorRepository, SectionProfessorRepository>();
        return serviceCollection;
    }
}