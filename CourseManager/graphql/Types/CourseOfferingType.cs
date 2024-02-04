using Data.Entities;
using graphql.DataLoaders;
using Repositories.Interfaces;

namespace graphql.Types;

public class CourseOfferingType : ObjectType<CourseOffering>
{
     protected override void Configure(IObjectTypeDescriptor<CourseOffering> descriptor)
     {
          descriptor.Field(co => co.Sections)
               .Resolve(context =>
               {
                    var sectionGroupedDataLoader = context.Service<SectionGroupedDataLoader>();
                    return sectionGroupedDataLoader.LoadAsync(context.Parent<CourseOffering>().Id, context.RequestAborted);
               });
     }
}