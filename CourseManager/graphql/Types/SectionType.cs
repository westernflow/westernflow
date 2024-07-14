using System.Collections.ObjectModel;
using Data.Entities;
using graphql.DataLoaders;
using Repositories.Interfaces;

namespace graphql.Types;

public class SectionType : ObjectType<Section>
{
    protected override void Configure(IObjectTypeDescriptor<Section> descriptor)
    {
        descriptor.Field(s => s.TimingDetails)
            .Resolve(context =>
            {
                var timingDetailsGroupedDataLoader = context.Service<TimingDetailsGroupedDataLoader>();
                return timingDetailsGroupedDataLoader.LoadAsync(context.Parent<Section>().Id, context.RequestAborted);
            });
        
        descriptor.Field(s => s.Professors)
            .Resolve(context =>
            {
                var professorGroupedDataLoader = context.Service<ProfessorGroupedDataLoader>();
                return professorGroupedDataLoader.LoadAsync(context.Parent<Section>().Id, context.RequestAborted);
            });

        descriptor.Field(s => s.CourseOffering)
            .Resolve(async context =>
            {
                var courseOfferingRepository = context.Service<ICourseOfferingRepository>();
                return await courseOfferingRepository.GetSingleOrDefaultAsync(co =>
                    co.Id == context.Parent<Section>().CourseOfferingId);
            });

    }
}