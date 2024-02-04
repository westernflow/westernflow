using Data.Entities;
using Repositories.Interfaces;

namespace graphql.DataLoaders;

public class CourseBatchDataLoader : BatchDataLoader<int, Course>
{
    private readonly ICourseRepository _courseRepository;
    
    public CourseBatchDataLoader(ICourseRepository courseRepository, IBatchScheduler batchScheduler, DataLoaderOptions? options = null) : base(batchScheduler, options)
    {
        _courseRepository = courseRepository;
    }
    
    protected override async Task<IReadOnlyDictionary<int, Course>> LoadBatchAsync(IReadOnlyList<int> keys, CancellationToken cancellationToken)
    {
        var courses = await _courseRepository.GetByConditionAsync(c => keys.Contains(c.Id));
        return courses.ToDictionary(c => c.Id);
    }
}