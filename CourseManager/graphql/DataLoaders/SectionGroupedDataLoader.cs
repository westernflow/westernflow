using Data.Entities;
using Repositories.Interfaces;

namespace graphql.DataLoaders;

public class SectionGroupedDataLoader : GroupedDataLoader<int, Section>
{
    private readonly ISectionRepository _sectionRepository;

    public SectionGroupedDataLoader(
        ISectionRepository sectionRepository, 
        IBatchScheduler batchScheduler, 
        DataLoaderOptions? options = null) : base(batchScheduler, options)
    {
        _sectionRepository = sectionRepository;
    }
    
    protected override async Task<ILookup<int, Section>> LoadGroupedBatchAsync(
        IReadOnlyList<int> keys, 
        CancellationToken cancellationToken)
    {
        var sections = await _sectionRepository.GetByConditionAsync(x => keys.Contains(x.CourseOfferingId));
        return sections.ToLookup(x => x.CourseOfferingId);
    }
}