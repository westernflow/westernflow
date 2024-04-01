using Data;
using Data.Entities.JoinTables;
using Microsoft.EntityFrameworkCore;
using Repositories.Interfaces.JoinTables;

namespace Repositories.Repositories.JoinTables;

public class SectionProfessorRepository : GenericRepository<JoinedSectionProfessor>, ISectionProfessorRepository
{
    public SectionProfessorRepository(IDbContextFactory<CourseManagerDbContext> context) : base(context)
    {
    }
}