using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Data.Interfaces;

namespace Data.Entities;

public class Course : IEntity
{
    // see https://stackoverflow.com/questions/54400115/no-suitable-constructor-found-for-entity-type-string
    private Course()
    {
    }

    [Key]
    public int Id { get; set; }
    
    public int Number { get; set; }
    [StringLength(10)]
    public string Suffix { get; set; }
    [StringLength(100)]
    public string Name { get; set; }
    [StringLength(1000)]
    public string? PrerequisiteString { get; set; }
    [StringLength(1000)]
    public string? CorequisiteString { get; set; }
    [StringLength(1000)]
    public string? AntirequisiteString { get; set; }
    [StringLength(2000)]
    public string? Description { get; set; }
    
    public int SourceInfoId { get; set; }
    [ForeignKey(nameof(SourceInfoId))]
    public SourceInfo Source { get; set; }
    
    public int FacultyId { get; set; }
    [ForeignKey(nameof(FacultyId))]
    public Faculty Faculty { get; set; }
    
    
    [InverseProperty(nameof(Section.Course))]
    public virtual ICollection<Section>? Sections { get; set; }
    
    public Course(Faculty faculty, SourceInfo source, string suffix, string name)
    {
        Faculty = faculty;
        Source = source;
        Suffix = suffix;
        Name = name;
    }
}