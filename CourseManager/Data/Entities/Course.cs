using System.ComponentModel.DataAnnotations;
using Data.Interfaces;

namespace Data.Entities;

public class Course : IEntity
{
    [Key]
    public int Id { get; set; }
    public SourceInfo Source { get; set; }

    public Faculty Faculty { get; set; }
    public int Number { get; set; }
    public string Suffix { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    
    public virtual ICollection<SectionComponent>? SectionData { get; set; }

    public Course(Faculty faculty, string suffix, string name)
    {
        Faculty = faculty;
        Suffix = suffix;
        Name = name;
    }
}