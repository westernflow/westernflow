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
    }
}