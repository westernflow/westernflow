using Data.Entities;
using Repositories.Interfaces;

namespace graphql.DataLoaders;

public class CourseReviewGroupedDataLoader : GroupedDataLoader<int, CourseReview>
{
    private readonly ICourseReviewRepository courseReviewRepository;
    
    public CourseReviewGroupedDataLoader(
        ICourseReviewRepository courseReviewRepository,
        IBatchScheduler batchScheduler,
        DataLoaderOptions? options = null)
        : base(batchScheduler, options)
    {
        this.courseReviewRepository = courseReviewRepository;
    }
    
    protected override async Task<ILookup<int, CourseReview>> LoadGroupedBatchAsync(
        IReadOnlyList<int> keys, 
        CancellationToken cancellationToken)
    {
        var courseReviews = await courseReviewRepository.GetByConditionAsync(x => keys.Contains(x.CourseId));
        return courseReviews.ToLookup(x => x.CourseId);
    }
}
    
    