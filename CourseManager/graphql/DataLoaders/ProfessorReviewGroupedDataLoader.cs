using Data.Entities;
using Repositories.Interfaces;

namespace graphql.DataLoaders;

public class ProfessorReviewGroupedDataLoader(
    IProfessorReviewRepository professorReviewRepository,
    IBatchScheduler batchScheduler,
    DataLoaderOptions? options = null)
    : GroupedDataLoader<int, ProfessorReview>(batchScheduler, options)
{
    protected override async Task<ILookup<int, ProfessorReview>> LoadGroupedBatchAsync(
        IReadOnlyList<int> keys, 
        CancellationToken cancellationToken)
    {
        var professorReviews = await professorReviewRepository.GetByConditionAsync(x => keys.Contains(x.ProfessorId));
        return professorReviews.ToLookup(x => x.ProfessorId);
    }
}