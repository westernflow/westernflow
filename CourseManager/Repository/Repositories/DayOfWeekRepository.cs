using Data;
using Data.Entities.EnumTables;
using Microsoft.EntityFrameworkCore;
using Repositories.Interfaces;

namespace Repositories.Repositories;

public class DayOfWeekRepository : GenericRepository<DayOfWeekEnumEntity>, IDayOfWeekRepository
{
    public DayOfWeekRepository(IDbContextFactory<CourseManagerDbContext> dbContextFactory) : base(dbContextFactory)
    {
    }
}