using System.Collections.ObjectModel;
using Data;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Repositories.Interfaces;

namespace Repositories.Repositories;

public class CourseRepository : GenericRepository<Course>, ICourseRepository
{
    private readonly IDbContextFactory<CourseManagerDbContext> _contextFactory;

    public CourseRepository(IDbContextFactory<CourseManagerDbContext> contextFactory) : base(contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<Course> GetByCodeAsync(string facultyName, int code)
    {
        using (var dbContext = await _contextFactory.CreateDbContextAsync())
        {
            return await dbContext.Set<Course>()
                .Include(c => c.Faculty)
                .FirstOrDefaultAsync(c => c.Faculty.Abbreviation == facultyName && c.Number == code);
        }
    }
}