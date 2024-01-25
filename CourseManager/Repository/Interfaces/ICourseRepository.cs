using Data.Entities;

namespace Repositories.Interfaces;

public interface ICourseRepository : IGenericRepository<Course>
{
    public Task<IReadOnlyCollection<Course>> GetAllAsync();
}