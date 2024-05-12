using graphql.DataLoaders;
using graphql.Resolvers;
using graphql.Types;

namespace graphql.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddGqlTypes(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddGraphQLServer()
            .AddQueryType<Query>()
            .AddMutationType<Mutation>()
            
            .AddType<CourseType>()
            .AddType<CourseOfferingType>()
            .AddType<SectionType>()
            .AddType<ProfessorType>()
            
            .AddDataLoader<CourseBatchDataLoader>()
            .AddDataLoader<FacultyBatchDataLoader>()
            .AddDataLoader<CourseOfferingGroupedDataLoader>()
            .AddDataLoader<SectionGroupedDataLoader>()
            .AddDataLoader<TimingDetailsGroupedDataLoader>()
            .AddDataLoader<CourseReviewGroupedDataLoader>()
            .AddDataLoader<ProfessorReviewGroupedDataLoader>()
            
            .AddResolver<ProfessorResolver>()
            .AddResolver<CourseResolver>()
            
            .AddMutationConventions()
            
            .AddFiltering()
            .AddProjections()
            .ModifyRequestOptions(o =>
                o.IncludeExceptionDetails = true);
        return serviceCollection;
    }
}