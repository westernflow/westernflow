using Data.Entities;
using Repositories.Interfaces;

namespace graphql;

public class Query
{
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    public async Task<IEnumerable<Course>> GetCoursesAsync([Service] ICourseRepository courseRepository)
        => await courseRepository.GetAllAsync();
}
