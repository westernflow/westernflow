using Data.Entities;
using Repositories.Interfaces;

namespace graphql.DataLoaders;

public class TimingDetailsGroupedDataLoader : GroupedDataLoader<int, TimingDetails>
{
    private readonly ITimingDetailsRepository _timingDetailsRepository;
    
    public TimingDetailsGroupedDataLoader(ITimingDetailsRepository timingDetailsRepository, IBatchScheduler batchScheduler, DataLoaderOptions? options = null) : base(batchScheduler, options)
    {
        _timingDetailsRepository = timingDetailsRepository;
    }
    
    protected override async Task<ILookup<int, TimingDetails>> LoadGroupedBatchAsync(IReadOnlyList<int> keys, CancellationToken cancellationToken)
    {
        var timingDetails = await _timingDetailsRepository.GetByConditionAsync(x => keys.Contains(x.SectionId));
        return timingDetails.ToLookup(x => x.SectionId);
    }
}