using Data.Entities;
using Repositories.Interfaces;

namespace graphql.DataLoaders;

public class CourseOfferingGroupedDataLoader : GroupedDataLoader<int, CourseOffering>
{
    private readonly ICourseOfferingRepository _courseOfferingRepository;
    
    public CourseOfferingGroupedDataLoader(
        ICourseOfferingRepository courseOfferingRepository, 
        IBatchScheduler batchScheduler, 
        DataLoaderOptions? options = null) : base(batchScheduler, options)
    {
        _courseOfferingRepository = courseOfferingRepository;
    }
    
    protected override async Task<ILookup<int, CourseOffering>> LoadGroupedBatchAsync(
        IReadOnlyList<int> keys, 
        CancellationToken cancellationToken)
    {
        var courseOfferings = await _courseOfferingRepository.GetByConditionAsync(x => keys.Contains(x.CourseId));
        return courseOfferings.ToLookup(x => x.CourseId);
    }
}