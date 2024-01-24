using Data.Entities;
using Repositories.Interfaces;

namespace graphql;

public class Query
{
    [UseFiltering]
    public async Task<Course?> GetCoursesAsync([Service] ICourseRepository courseRepository)
        => await courseRepository.GetSingleOrDefaultAsync(c => c.Id == 1);
}
