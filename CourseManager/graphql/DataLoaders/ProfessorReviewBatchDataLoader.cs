using Data.Entities;
using Repositories.Interfaces;

namespace graphql.DataLoaders;

public class ProfessorReviewGroupedDataLoader : GroupedDataLoader<int, ProfessorReview>
{
    private readonly IProfessorReviewRepository _professorReviewRepository;
    
    public ProfessorReviewGroupedDataLoader(
        IProfessorReviewRepository professorReviewRepository, 
        IBatchScheduler batchScheduler, 
        DataLoaderOptions? options = null) : base(batchScheduler, options)
    {
        _professorReviewRepository = professorReviewRepository;
    }
    
    protected override async Task<ILookup<int, ProfessorReview>> LoadGroupedBatchAsync(
        IReadOnlyList<int> keys, 
        CancellationToken cancellationToken)
    {
        var professorReviews = await _professorReviewRepository.GetByConditionAsync(x => keys.Contains(x.ProfessorId));
        return professorReviews.ToLookup(x => x.ProfessorId);
    }
}