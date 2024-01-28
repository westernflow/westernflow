using System.Collections.ObjectModel;
using Data;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Repositories.Interfaces;

namespace Repositories.Repositories;

public class CourseRepository : GenericRepository<Course>, ICourseRepository
{
    public CourseRepository(IDbContextFactory<CourseManagerDbContext> dbContextFactory) : base(dbContextFactory)
    {
    }
}