using Data.Entities;
using graphql.DataLoaders;
using graphql.Resolvers;

namespace graphql.Types;

public class CourseType : ObjectType<Course>
{
    public class CourseReviewAggregate 
    {
        public double AverageLiked { get; set; }
        public double AverageEasiness { get; set; }
        public double AverageUsefulness { get; set; }
        public int TotalComments { get; set; }
        public int TotalReviews { get; set; }
    }
    
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
                var facultyDataLoader = context.Service<FacultyBatchDataLoader>();
                var faculty = await facultyDataLoader.LoadAsync(course.FacultyId, context.RequestAborted);
                
                return faculty.Abbreviation + " " + course.Number;
            });
        
        
        descriptor.Field("rating").ResolveWith<CourseResolver>(r => r.GetRating(default!, default!));
    }
}