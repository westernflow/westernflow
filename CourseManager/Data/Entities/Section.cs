using System.ComponentModel.DataAnnotations;
using Data.Interfaces;

namespace Data.Entities;

public class Section :IEntity
{
    [Key]
    public int Id { get; set; }
    public SourceInfo Source { get; set; }
    public SectionComponent SectionData { get; set; }
    public Course Course { get; set; }

    public Section(Course course, SectionComponent sectionComponent, SourceInfo sourceInfo)
    {
        Source = sourceInfo;
        SectionData = sectionComponent;
        Course = course;
    }
}