using Data.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repositories.Interfaces;
using Repositories.Repositories;

namespace Repositories.Extensions;

public static class ServiceCollectionExtensions 
{
   public static IServiceCollection AddRepositories(this IServiceCollection serviceCollection)
   {
      serviceCollection.AddScoped<ICourseRepository, CourseRepository>();
      serviceCollection.AddScoped<IFacultyRepository, FacultyRepository>();
      serviceCollection.AddScoped<IProfessorRepository, ProfessorRepository>();
      serviceCollection.AddScoped<ISectionComponentRepository, SectionComponentRepository>();
      serviceCollection.AddScoped<ISectionRepository, SectionRepository>();
      serviceCollection.AddScoped<ISourceInfoRepository, SourceInfoRepository>();
      return serviceCollection;
   }
}