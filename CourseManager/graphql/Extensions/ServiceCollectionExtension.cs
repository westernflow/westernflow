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
            
            .AddResolver<ProfessorResolver>()
            
            .AddMutationConventions()
            .AddGlobalObjectIdentification()
            
            .AddFiltering()
            .AddProjections()
            .ModifyRequestOptions(o =>
                o.IncludeExceptionDetails = true);
        return serviceCollection;
    }
}