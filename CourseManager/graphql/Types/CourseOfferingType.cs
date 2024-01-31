using Data.Entities;
using Repositories.Interfaces;

namespace graphql.Types;

public class CourseOfferingType : ObjectType<CourseOffering>
{
     protected override void Configure(IObjectTypeDescriptor<CourseOffering> descriptor)
     {
          descriptor.Field(f => f.Sections).ResolveWith<Resolvers>(r => r.GetSections(default!, default!));
     }
     
     private class Resolvers
     {
          public async Task<IEnumerable<Section>> GetSections([Parent] CourseOffering courseOffering, [Service] ISectionRepository sectionRepository)
          {
               return await sectionRepository.GetByConditionAsync(s => s.CourseOfferingId == courseOffering.Id);
          }
     }
}