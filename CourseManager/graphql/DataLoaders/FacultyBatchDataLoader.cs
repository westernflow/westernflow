using Data.Entities;
using Repositories.Interfaces;

namespace graphql.DataLoaders;

public class FacultyBatchDataLoader : BatchDataLoader<int, Faculty>
{   
    private readonly IFacultyRepository _facultyRepository;
    
    public FacultyBatchDataLoader(IFacultyRepository facultyRepository, IBatchScheduler batchScheduler, DataLoaderOptions? options = null) : base(batchScheduler, options)
    {
        _facultyRepository = facultyRepository;
    }
    
    protected override async Task<IReadOnlyDictionary<int, Faculty>> LoadBatchAsync(IReadOnlyList<int> keys, CancellationToken cancellationToken)
    {
        var faculties = await _facultyRepository.GetByConditionAsync(x => keys.Contains(x.Id));
        return faculties.ToDictionary(x => x.Id);
    }
}