using Business.Interfaces;
using Business.Providers;
using Data.Entities;
using graphql.DataLoaders;
using Repositories.Interfaces;

namespace graphql;

public class Query
{
    public string GetJwtAsync([Service] IReviewerInfoProvider identityService)
        => identityService.GetJWTAsync();

    public async Task<Course?> GetCourseByCodeAsync(string facultyAbbreviation, int code,
        [Service] ICourseRepository courseRepository)
        => await courseRepository.GetByCodeAsync(facultyAbbreviation, code);
    
    [UseProjection]
    [UseFiltering]
    public IQueryable<Professor> GetProfessors([Service] IProfessorRepository professorRepository)
    {
        return professorRepository.GetQueryable();
    }

    [NodeResolver]
    public async Task<Course> GetCourseByIdAsync(int id, [Service] ICourseRepository courseRepository)
        => await courseRepository.GetByIdAsync(id);
    
    [NodeResolver]
    public async Task<Faculty> GetFacultyByIdAsync(int id, [Service] IFacultyRepository facultyRepository)
        => await facultyRepository.GetByIdAsync(id);
    
    [NodeResolver]
    public async Task<CourseOffering> GetCourseOfferingByIdAsync(int id, [Service] ICourseOfferingRepository courseOfferingRepository)
        => await courseOfferingRepository.GetByIdAsync(id);
    
    [NodeResolver]
    public async Task<Section> GetSectionByIdAsync(int id, [Service] ISectionRepository sectionRepository)
        => await sectionRepository.GetByIdAsync(id);
    
    [NodeResolver]
    public async Task<Reviewer> GetReviewerByIdAsync(int id, [Service] IReviewerRepository reviewerRepository)
        => await reviewerRepository.GetByIdAsync(id);
    
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