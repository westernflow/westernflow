using Data;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Repositories.Interfaces;

namespace Repositories.Repositories;

public class TimingDetailsRepository : GenericRepository<TimingDetails>, ITimingDetailsRepository
{
    private readonly IDbContextFactory<CourseManagerDbContext> _contextFactory;
    
    public TimingDetailsRepository(IDbContextFactory<CourseManagerDbContext> contextFactory) : base(contextFactory)
    {
        _contextFactory = contextFactory;
    }
}