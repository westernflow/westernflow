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
        descriptor.Field(f => f.Source).ResolveWith<Resolvers>(r => r.GetSource(default!, default!));
    }
    
    private class Resolvers
    {
        public async Task<Faculty> GetFaculty([Parent] Course course, [Service] IFacultyRepository facultyRepository)
        {
            return await facultyRepository.GetByIdAsync(course.FacultyId);
        }
        
        public async Task<SourceInfo> GetSource([Parent] Course course, [Service] ISourceInfoRepository sourceRepository)
        {
            return await sourceRepository.GetByIdAsync(course.SourceInfoId);
        }
    }
}