using Data.Entities;
using graphql.DataLoaders;
using HotChocolate.Resolvers;
using Repositories.Interfaces;

namespace graphql;

public class Query
{
    [UsePaging(MaxPageSize = 5000)]
    [UseProjection]
    [UseFiltering]
    public async Task<IEnumerable<Course>> GetCoursesAsync([Service] ICourseRepository courseRepository)
        => await courseRepository.GetAllAsync();
    
    public async Task<Course> GetCourseByCodeAsync(string facultyAbbreviation, int code, [Service] ICourseRepository courseRepository)
        => await courseRepository.GetByCodeAsync(facultyAbbreviation, code);
    
    [NodeResolver]
    public async Task<Course> GetCourseByIdAsync(int id, [Service] ICourseRepository courseRepository)
        => await courseRepository.GetByIdAsync(id);
    
    [NodeResolver]
    public async Task<Faculty> GetFacultyByIdAsync(int id, [Service] IFacultyRepository facultyRepository)
        => await facultyRepository.GetByIdAsync(id);
    
    public async Task<Course> GetCourseByIdAsync(int id, CourseBatchDataLoader courseBatchDataLoader, CancellationToken cancellationToken)
        => await courseBatchDataLoader.LoadAsync(id, cancellationToken); 
    
    public async Task<Faculty> GetFacultyByIdAsync(int id, FacultyBatchDataLoader facultyBatchDataLoader, CancellationToken cancellationToken)
        => await facultyBatchDataLoader.LoadAsync(id, cancellationToken);
    
    public async Task<IEnumerable<CourseOffering>> GetCourseOfferingsByCourseIdAsync(int courseId, CourseOfferingGroupedDataLoader courseOfferingGroupedDataLoader, CancellationToken cancellationToken)
        => await courseOfferingGroupedDataLoader.LoadAsync(courseId, cancellationToken);
    
    public async Task<IEnumerable<Section>> GetSectionsByCourseOfferingIdAsync(int courseOfferingId, SectionGroupedDataLoader sectionGroupedDataLoader, CancellationToken cancellationToken)
        => await sectionGroupedDataLoader.LoadAsync(courseOfferingId, cancellationToken);
    
    public async Task<IEnumerable<TimingDetails>> GetTimingDetailsBySectionIdAsync(int sectionId, TimingDetailsGroupedDataLoader timingDetailsGroupedDataLoader, CancellationToken cancellationToken)
        => await timingDetailsGroupedDataLoader.LoadAsync(sectionId, cancellationToken);
}