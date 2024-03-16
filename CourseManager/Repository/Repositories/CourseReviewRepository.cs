using Data;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Repositories.Interfaces;

namespace Repositories.Repositories;

public class CourseReviewRepository : GenericRepository<CourseReview>, ICourseReviewRepository
{
    public CourseReviewRepository(IDbContextFactory<CourseManagerDbContext> contextFactory) : base(contextFactory)
    {
    }
}