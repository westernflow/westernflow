using Data;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Repositories.Interfaces;

namespace Repositories.Repositories;

public class SectionComponentRepository : GenericRepository<SectionComponent>, ISectionComponentRepository
{
   public SectionComponentRepository(IDbContextFactory<CourseManagerDbContext> dbContextFactory) : base(dbContextFactory)
   {
   } 
}