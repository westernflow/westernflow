using Data;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Repositories.Interfaces;

namespace Repositories.Repositories;

public class ProfessorReviewRepository : GenericRepository<ProfessorReview>, IProfessorReviewRepository 
{
    public ProfessorReviewRepository(IDbContextFactory<CourseManagerDbContext> dbContextFactory) : base(
        dbContextFactory)
    {
    }
}