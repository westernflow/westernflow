using Data.Entities;
using graphql.DataLoaders;
using graphql.Resolvers;
using Repositories.Interfaces;

namespace graphql.Types;

public class ProfessorType : ObjectType<Professor>
{
    protected override void Configure(IObjectTypeDescriptor<Professor> descriptor)
    {
        descriptor.Field("rating")
            .ResolveWith<ProfessorResolver>(r => r.GetRatingAsync(default!, default!));
        descriptor.Field("profCourses")
            .ResolveWith<ProfessorResolver>(r => r.GetProfCoursesAsync(default!));
        
    }

    public class ReviewAggregate
    {
        public double AverageQuality { get; set; }
        public double AverageDifficulty { get; set; }
        public double AverageHelpfulness { get; set; }
        public double AverageClarity { get; set; }
        public int TotalComments { get; set; }
        public int TotalReviews { get; set; }
    }
}