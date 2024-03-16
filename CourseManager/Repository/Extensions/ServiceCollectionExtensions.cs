using Data.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repositories.Interfaces;
using Repositories.Repositories;

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
      return serviceCollection;
   }
}