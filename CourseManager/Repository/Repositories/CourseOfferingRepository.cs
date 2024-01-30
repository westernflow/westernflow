using Data;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Repositories.Interfaces;

namespace Repositories.Repositories;

public class CourseOfferingRepository : GenericRepository<CourseOffering>, ICourseOfferingRepository
{
    private readonly IDbContextFactory<CourseManagerDbContext> _contextFactory;
    
    public CourseOfferingRepository(IDbContextFactory<CourseManagerDbContext> contextFactory) : base(contextFactory)
    {
        _contextFactory = contextFactory;
    }
}