using Business.Models.Inputs;
using Data;
using Data.Entities;

namespace graphql;

public class Mutation
{
    public async Task<CourseReview> CreateCourseReviewAsync(
        [Service] CourseManagerDbContext dbContext,
        CreateCourseReviewInput input,
        CancellationToken cancellationToken)
    {
        var courseReview = new CourseReview
        {
            IsLiked = input.IsLiked,
            EasyRating = input.EasyRating,
            UsefulRating = input.UsefulRating,
            ReviewText = input.ReviewText ?? string.Empty,
            ProfessorId = input.ProfessorId
        };
        dbContext.CourseReviews.Add(courseReview);
        await dbContext.SaveChangesAsync(cancellationToken);
        return courseReview;
    }
}