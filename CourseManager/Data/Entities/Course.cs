using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Data.Enums;
using Data.Interfaces;

namespace Data.Entities;

public record CourseConstructorParams()
{
    public int Number { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? PrerequisiteString { get; init; }
    public string? CorequisiteString { get; init; }
    public string? AntirequisiteString { get; init; }
    public string? Description { get; init; }
    public string? ExtraInformation { get; init; }
    public decimal? Weight { get; init; }
    public BreadthCategory BreadthCategory { get; init; }
    public string InternalCourseId { get; init; } = string.Empty;
    public int? SourceInfoId { get; init; }
    public int FacultyId { get; init; }
}

public class Course : IEntity
{
    // see https://stackoverflow.com/questions/54400115/no-suitable-constructor-found-for-entity-type-string
    private Course()
    {
    }
    
    public Course([NotNull] CourseConstructorParams courseConstructorParams)
    {
        Number = courseConstructorParams.Number;
        Name = courseConstructorParams.Name;
        PrerequisiteString = courseConstructorParams.PrerequisiteString;
        CorequisiteString = courseConstructorParams.CorequisiteString;
        AntirequisiteString = courseConstructorParams.AntirequisiteString;
        Description = courseConstructorParams.Description;
        ExtraInformation = courseConstructorParams.ExtraInformation;
        Weight = courseConstructorParams.Weight;
        BreadthCategory = courseConstructorParams.BreadthCategory;
        InternalCourseId = courseConstructorParams.InternalCourseId;
        SourceInfoId = courseConstructorParams.SourceInfoId;
        FacultyId = courseConstructorParams.FacultyId;
    }

    [Key]
    public int Id { get; set; }
    
    public int Number { get; set; }
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
    
    [Column(TypeName = "decimal(2,2)")]
    public decimal? Weight { get; set; } 
    public BreadthCategory BreadthCategory { get; set; }
    [StringLength(20)]
    public string InternalCourseId { get; set; } = string.Empty;
    
    public int? SourceInfoId { get; set; }
    [ForeignKey(nameof(SourceInfoId))]
    public virtual SourceInfo? Source { get; set; } = null!;
    
    public int FacultyId { get; set; }
    [ForeignKey(nameof(FacultyId))]
    public Faculty Faculty { get; set; } = null!;

    [InverseProperty(nameof(CourseOffering.Course))]
    public virtual ICollection<CourseOffering> CourseOfferings { get; set; } = null!;
    
    [InverseProperty(nameof(ProfessorReview.Course))]
    public virtual ICollection<ProfessorReview> RelatedProfessorReviews { get; set; } = null!;
}