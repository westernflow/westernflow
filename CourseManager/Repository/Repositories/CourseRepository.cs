using System.Collections.ObjectModel;
using Data;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Repositories.Interfaces;

namespace Repositories.Repositories;

public class CourseRepository : GenericRepository<Course>, ICourseRepository
{
   private readonly IDbContextFactory<CourseManagerDbContext> _dbContextFactory;
   
   public CourseRepository(IDbContextFactory<CourseManagerDbContext> dbContextFactory) : base(dbContextFactory)
   {
      _dbContextFactory = dbContextFactory;
   }
   
   public async Task<IReadOnlyCollection<Course>> GetAllAsync()
   {
      var courseList =  (await _dbContextFactory.CreateDbContextAsync()).Set<Course>().ToListAsync();
      return new ReadOnlyCollection<Course>(await courseList);
   }
}