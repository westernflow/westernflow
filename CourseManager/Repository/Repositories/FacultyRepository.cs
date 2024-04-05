using Data;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Repositories.Interfaces;

namespace Repositories.Repositories;

public class FacultyRepository : GenericRepository<Faculty>, IFacultyRepository
{
   private readonly IDbContextFactory<CourseManagerDbContext> _dbContextFactory;
   public FacultyRepository(IDbContextFactory<CourseManagerDbContext> dbContextFactory) : base(dbContextFactory)
   {  
      _dbContextFactory = dbContextFactory;
   }

   public new async Task InsertRangeAsync(IReadOnlyCollection<Faculty> entities)
   {
      using (var dbContext = await _dbContextFactory.CreateDbContextAsync())
      {
         foreach (var entity in entities)
         {
            var existingEntity = await GetSingleOrDefaultAsync(e => e.Abbreviation == entity.Abbreviation);
            if (existingEntity == null)
            {
               await dbContext.AddAsync(entity);
            }
            else
            {
               entity.Id = existingEntity.Id;
            }
         }
         
         await dbContext.SaveChangesAsync();
      }
   }
}