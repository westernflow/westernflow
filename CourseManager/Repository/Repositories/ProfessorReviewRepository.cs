using Data;
using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Repositories.Repositories;

internal class ProfessorReviewRepository : GenericRepository<ProfessorReview>
{
    public ProfessorReviewRepository(IDbContextFactory<CourseManagerDbContext> dbContextFactory) : base(
        dbContextFactory)
    {
    }
}