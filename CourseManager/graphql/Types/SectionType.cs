using System.Collections.ObjectModel;
using Data.Entities;
using Repositories.Interfaces;

namespace graphql.Types;

public class SectionType : ObjectType<Section>
{
    protected override void Configure(IObjectTypeDescriptor<Section> descriptor)
    {
        descriptor.Field(f => f.TimingDetails).ResolveWith<Resolvers>(r => r.GetTimingDetails(default!, default!));
    }
    
    private class Resolvers
    {
        public async Task<IReadOnlyCollection<TimingDetails>> GetTimingDetails([Parent] Section section, [Service] ITimingDetailsRepository timingDetailRepository)
        {
            return await timingDetailRepository.GetByConditionAsync(td => td.SectionId == section.Id);
        }
    }
}