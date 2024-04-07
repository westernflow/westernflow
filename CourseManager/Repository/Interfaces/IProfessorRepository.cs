using Data.Entities;

namespace Repositories.Interfaces;

public interface IProfessorRepository : IGenericRepository<Professor>
{
    Task AddSection(Professor entity, Section section);
}