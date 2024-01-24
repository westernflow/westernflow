using Data;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Repositories.Interfaces;

namespace Repositories.Repositories;

internal class SectionRepository : GenericRepository<Section>, ISectionRepository
{
    public SectionRepository(IDbContextFactory<CourseManagerDbContext> dbContextFactory) : base(dbContextFactory)
    {
    }
}