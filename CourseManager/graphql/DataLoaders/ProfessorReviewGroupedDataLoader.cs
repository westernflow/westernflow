using Data.Entities;
using Repositories.Interfaces;

namespace graphql.DataLoaders;

public class ProfessorReviewGroupedDataLoader : GroupedDataLoader<int, ProfessorReview>
{
    private readonly IProfessorReviewRepository professorReviewRepository;
    
    public ProfessorReviewGroupedDataLoader(
        IProfessorReviewRepository professorReviewRepository,
        IBatchScheduler batchScheduler,
        DataLoaderOptions? options = null)
        : base(batchScheduler, options)
    {
        this.professorReviewRepository = professorReviewRepository;
    }
    
    protected override async Task<ILookup<int, ProfessorReview>> LoadGroupedBatchAsync(
        IReadOnlyList<int> keys, 
        CancellationToken cancellationToken)
    {
        var professorReviews = await professorReviewRepository.GetByConditionAsync(x => keys.Contains(x.ProfessorId));
        return professorReviews.ToLookup(x => x.ProfessorId);
    }
}