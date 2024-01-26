using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Data.Enums;
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
    public CourseSuffix CourseSuffix { get; set; }
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;
    [StringLength(1000)]
    public string? PrerequisiteString { get; set; }
    [StringLength(1000)]
    public string? CorequisiteString { get; set; }
    [StringLength(1000)]
    public string? AntirequisiteString { get; set; }
    [StringLength(2000)]
    public string? Description { get; set; }
    [StringLength(1000)]
    public string? ExtraInformation { get; set; }
    public int? CourseWeight { get; set; } 
    public BreadthCategory BreadthCategory { get; set; }
    
    public int SourceInfoId { get; set; }
    [ForeignKey(nameof(SourceInfoId))]
    public virtual SourceInfo Source { get; set; } = null!;
    
    public int FacultyId { get; set; }
    [ForeignKey(nameof(FacultyId))]
    public Faculty Faculty { get; set; } = null!;

    [InverseProperty(nameof(Section.Course))]
    public virtual ICollection<Section> Sections { get; set; } = null!;

    [InverseProperty(nameof(ProfessorReview.Course))]
    public virtual ICollection<ProfessorReview> RelatedProfessorReviews { get; set; } = null!;
    
    public Course(Faculty faculty, string name)
    {
        Faculty = faculty;
        Name = name;
    }
}