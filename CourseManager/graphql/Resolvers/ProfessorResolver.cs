using Data.Entities;
using graphql.DataLoaders;
using graphql.Types;

namespace graphql.Resolvers;

public class ProfessorResolver
{
    [GraphQLName("profCourses")]
    public async Task<IReadOnlyCollection<Course>> GetProfCoursesAsync([Parent] Professor professor)
    {
        return new List<Course>();
    }

    [GraphQLName("rating")]
    public async Task<ProfessorType.ReviewAggregate> GetRatingAsync([Parent] Professor professor,
        [Service] ProfessorReviewGroupedDataLoader reviewDataLoader)
    {
        var reviews = await reviewDataLoader.LoadAsync(professor.Id);

        var reviewCount = reviews.Length;
        if (reviewCount == 0)
        {
            return new ProfessorType.ReviewAggregate()
            {
                AverageQuality = 0,
                AverageDifficulty = 0,
                AverageHelpfulness = 0,
                AverageClarity = 0,
                TotalComments = 0,
                TotalReviews = 0
            };
        }
        
        // quality, difficulty, helpfulness, clarity are all out of 1-5,
        // we want final average rating to be out of 0-1

        var averageQuality = reviews.Average(review => review.Quality) / 5;
        var averageDifficulty = reviews.Average(review => review.Difficulty) / 5;
        var averageHelpfulness = reviews.Average(review => review.Helpful) / 5;
        var averageClarity = reviews.Average(review => review.Clarity) / 5;
        var totalComments = reviews.Count(review => !string.IsNullOrWhiteSpace(review.ReviewText));

        return new ProfessorType.ReviewAggregate
        {
            AverageQuality = averageQuality,
            AverageDifficulty = averageDifficulty,
            AverageHelpfulness = averageHelpfulness,
            AverageClarity = averageClarity,
            TotalComments = totalComments,
            TotalReviews = reviewCount
        };
    }
}