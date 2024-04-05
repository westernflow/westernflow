using System.Linq.Expressions;
using Data.Entities;

namespace Repositories.Interfaces;

public interface ICourseRepository : IGenericRepository<Course>
{
    Task<Course?> GetByCodeAsync(string facultyName, int code);
    Task UpsertAsync(Course entity, params Expression<Func<Course, object>>[] properties);
}