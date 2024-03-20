using Data.Entities;

namespace Repositories.Interfaces;

public interface ICourseRepository : IGenericRepository<Course>
{
    Task<Course> GetByCodeAsync(string facultyName, int code);
}