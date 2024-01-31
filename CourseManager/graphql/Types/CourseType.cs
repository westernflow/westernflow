using Data;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Repositories.Interfaces;

namespace graphql.Types;

public class CourseType : ObjectType<Course>
{
    protected override void Configure(IObjectTypeDescriptor<Course> descriptor)
    {
        descriptor.Field(f => f.Faculty).ResolveWith<Resolvers>(r => r.GetFaculty(default!, default!));
        descriptor.Field(f => f.CourseOfferings).ResolveWith<Resolvers>(r => r.GetCourseOfferings(default!, default!));
    }
    
    private class Resolvers
    {
        public async Task<Faculty> GetFaculty([Parent] Course course, [Service] IFacultyRepository facultyRepository)
        {
            return await facultyRepository.GetByIdAsync(course.FacultyId);
        }
        
        public async Task<IEnumerable<CourseOffering>> GetCourseOfferings([Parent] Course course, [Service] ICourseOfferingRepository courseOfferingRepository)
        {
            return await courseOfferingRepository.GetByConditionAsync(co => co.CourseId == course.Id);
        }
    }
}