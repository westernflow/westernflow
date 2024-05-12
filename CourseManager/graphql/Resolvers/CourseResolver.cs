using Data.Entities;
using graphql.DataLoaders;
using graphql.Types;

namespace graphql.Resolvers;

public class CourseResolver
{
    [GraphQLName("rating")]
    public async Task<CourseType.CourseReviewAggregate> GetRating([Parent] Course course,
        [Service] CourseReviewGroupedDataLoader reviewDataLoader)
    {
        var reviews = await reviewDataLoader.LoadAsync(course.Id, default);

        var reviewCount = reviews.Length;
        if (reviewCount == 0)
        {
            return new CourseType.CourseReviewAggregate
            {
                AverageLiked = 0,
                AverageEasiness = 0,
                AverageUsefulness = 0,
                TotalComments = 0,
                TotalReviews = 0
            };
        }

        var averageLiked = reviews.Average(review => review.IsLiked ? 1 : 0);
        var averageEasiness = reviews.Average(review => review.EasyRating) / 5;
        var averageUsefulness = reviews.Average(review => review.UsefulRating) / 5;
        var totalComments = reviews.Count(review => !string.IsNullOrWhiteSpace(review.ReviewText));

        return new CourseType.CourseReviewAggregate
        {
            AverageLiked = averageLiked,
            AverageEasiness = averageEasiness,
            AverageUsefulness = averageUsefulness,
            TotalComments = totalComments,
            TotalReviews = reviewCount
        };
    }
}