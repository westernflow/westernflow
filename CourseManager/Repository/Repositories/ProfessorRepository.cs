using Data;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Repositories.Interfaces;

namespace Repositories.Repositories;

internal class ProfessorRepository : GenericRepository<Professor>, IProfessorRepository
{
    private readonly IDbContextFactory<CourseManagerDbContext> _dbContextFactory;

    public ProfessorRepository(IDbContextFactory<CourseManagerDbContext> dbContextFactory) : base(dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }
    
    public new async Task InsertAsync(Professor entity)
    {
        using (var dbContext = await _dbContextFactory.CreateDbContextAsync())
        {
            foreach (var section in entity.Sections)
            {
                dbContext.Entry(section).State = EntityState.Unchanged;
            }
            
            await dbContext.Professors.AddAsync(entity);
            await dbContext.SaveChangesAsync();
        }
    }
}