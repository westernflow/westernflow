using Data.Entities;
using Repositories.Interfaces;

namespace graphql.DataLoaders;

public class ProfessorGroupedDataLoader(
    IProfessorRepository professorRepository,
    IBatchScheduler batchScheduler,
    DataLoaderOptions? options = null)
    : GroupedDataLoader<int, Professor>(batchScheduler, options)
{
    protected override async Task<ILookup<int, Professor>> LoadGroupedBatchAsync(
        IReadOnlyList<int> keys, 
        CancellationToken cancellationToken)
    {
        var professors = await professorRepository.GetByConditionAsync(x => keys.Contains(x.Id));
        return professors.ToLookup(x => x.Id);
    }
}