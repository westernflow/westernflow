using Data;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Repositories.Interfaces;

namespace Repositories.Repositories;

public class ReviewerRepository : GenericRepository<Reviewer>, IReviewerRepository
{
    public ReviewerRepository(IDbContextFactory<CourseManagerDbContext> context) : base(context)
    {
    }
}