using System.Linq.Expressions;
using Data;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Repositories.Interfaces;

namespace Repositories.Repositories;

public class CourseRepository : GenericRepository<Course>, ICourseRepository
{
    private readonly IDbContextFactory<CourseManagerDbContext> _contextFactory;

    public CourseRepository(IDbContextFactory<CourseManagerDbContext> contextFactory) : base(contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<Course?> GetByCodeAsync(string facultyName, int code)
    {
        using (var dbContext = await _contextFactory.CreateDbContextAsync())
        {
            return await dbContext.Set<Course>()
                .Include(c => c.Faculty)
                .FirstOrDefaultAsync(c => c.Faculty.Abbreviation == facultyName && c.Number == code) ?? throw new InvalidOperationException();
        }
    }
    
    public async Task UpsertAsync(Course entity, params Expression<Func<Course, object>>[] properties)
    {
        using (var dbContext = await _contextFactory.CreateDbContextAsync())
        {
            // try to see if there exists an entity with same keys we care about already (facultyid + number)
            var existingEntity = await dbContext.Set<Course>()
                .FirstOrDefaultAsync(c => c.FacultyId == entity.FacultyId && c.Number == entity.Number);

            if (existingEntity == null)
            {
                await dbContext.AddAsync(entity);
                await dbContext.SaveChangesAsync();
            }
            else
            {
                await UpdateAsync(entity, dbContext, dbContext.Entry(existingEntity), properties);
            }
        }
    }
}