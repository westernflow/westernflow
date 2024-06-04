using Data.Entities;
using Repositories.Interfaces;

namespace graphql.DataLoaders;

public class ProfessorGroupedDataLoader : GroupedDataLoader<int, Professor>
{
    private readonly IProfessorRepository professorRepository;
    
    public ProfessorGroupedDataLoader(
        IProfessorRepository professorRepository,
        IBatchScheduler batchScheduler,
        DataLoaderOptions? options = null)
        : base(batchScheduler, options)
    {
        this.professorRepository = professorRepository;
    }
    
    protected override async Task<ILookup<int, Professor>> LoadGroupedBatchAsync(
        IReadOnlyList<int> keys, 
        CancellationToken cancellationToken)
    {
        var professors = await professorRepository.GetByConditionAsync(x => keys.Contains(x.Id));
        return professors.ToLookup(x => x.Id);
    }
}