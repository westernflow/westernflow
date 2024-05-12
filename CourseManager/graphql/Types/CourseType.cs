using Data;
using Data.Entities;
using graphql.DataLoaders;
using Microsoft.EntityFrameworkCore;
using Repositories.Interfaces;

namespace graphql.Types;

public class CourseType : ObjectType<Course>
{
    protected override void Configure(IObjectTypeDescriptor<Course> descriptor)
    {
        descriptor.Field(f => f.Faculty)
            .Resolve(context =>
            {
                var facultyDataLoader = context.Service<FacultyBatchDataLoader>();
                return facultyDataLoader.LoadAsync(context.Parent<Course>().FacultyId, context.RequestAborted);
            });

        descriptor.Field(f => f.CourseOfferings)
            .Resolve(context =>
            {
                var courseOfferingGroupedDataLoader = context.Service<CourseOfferingGroupedDataLoader>();
                return courseOfferingGroupedDataLoader.LoadAsync(context.Parent<Course>().Id, context.RequestAborted);
            });
        
        descriptor.Field("code").Type<NonNullType<StringType>>()
            .Resolve(async context =>
            {
                var course = context.Parent<Course>();
                return course.Faculty.Abbreviation + " " + course.Number;
            });
    }
}