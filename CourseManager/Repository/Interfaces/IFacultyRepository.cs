using Data.Entities;

namespace Repositories.Interfaces;

public interface IFacultyRepository : IGenericRepository<Faculty>
{
    Task InsertRangeAsync(IReadOnlyCollection<Faculty> faculties);
}