using Data;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Repositories.Interfaces;

namespace Repositories.Repositories;

internal class FacultyRepository : GenericRepository<Faculty>, IFacultyRepository
{
   public FacultyRepository(IDbContextFactory<CourseManagerDbContext> dbContextFactory) : base(dbContextFactory)
   {
   }
}