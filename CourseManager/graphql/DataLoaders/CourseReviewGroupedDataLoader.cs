using Data.Entities;
using Repositories.Interfaces;

namespace graphql.DataLoaders;

public class CourseReviewGroupedDataLoader(
    ICourseReviewRepository courseReviewRepository,
    IBatchScheduler batchScheduler,
    DataLoaderOptions? options = null)
    : GroupedDataLoader<int, CourseReview>(batchScheduler, options)
{
    protected override async Task<ILookup<int, CourseReview>> LoadGroupedBatchAsync(
        IReadOnlyList<int> keys, 
        CancellationToken cancellationToken)
    {
        var courseReviews = await courseReviewRepository.GetByConditionAsync(x => keys.Contains(x.CourseId));
        return courseReviews.ToLookup(x => x.CourseId);
    }
}
    
    