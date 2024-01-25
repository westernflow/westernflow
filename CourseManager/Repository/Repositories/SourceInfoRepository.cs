using System.Linq.Expressions;
using Data;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Repositories.Interfaces;

namespace Repositories.Repositories;

internal class SourceInfoRepository : GenericRepository<SourceInfo>, ISourceInfoRepository
{
    public SourceInfoRepository(IDbContextFactory<CourseManagerDbContext> dbContextFactory) : base(dbContextFactory)
    {
    }
}