using Data.Entities;

namespace Repositories.Interfaces;

public interface IFacultyRepository : IGenericRepository<Faculty>
{
    new Task InsertRangeAsync(IReadOnlyCollection<Faculty> faculties);
}