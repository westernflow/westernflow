using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Data.Interfaces;

namespace Data.Entities;

public class Course : IEntity
{
    // see https://stackoverflow.com/questions/54400115/no-suitable-constructor-found-for-entity-type-string
    public Course()
    {
    }
    
    [Key]
    public int Id { get; set; }
    
    public int Number { get; set; }
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;
    [StringLength(3000)]
    public string Prerequisites { get; set; } = string.Empty;
    [StringLength(3000)]
    public string Corequisites { get; set; } = string.Empty;
    [StringLength(3000)]
    public string Antirequisites { get; set; } = string.Empty;
    [StringLength(3000)]
    public string Description { get; set; } = string.Empty;
    [StringLength(3000)]
    public string ExtraInformation { get; set; } = string.Empty;
    
    [Column(TypeName = "decimal(3,2)")]
    public decimal? Weight { get; set; } 
    [StringLength(20)]
    public string InternalCourseId { get; set; } = string.Empty;
    
    public int FacultyId { get; set; }

    [ForeignKey(nameof(FacultyId))]
    public Faculty Faculty { get; set; } = null!;

    [InverseProperty(nameof(CourseOffering.Course))]
    public ICollection<CourseOffering> CourseOfferings { get; set; } = new List<CourseOffering>();

    [InverseProperty(nameof(ProfessorReview.Course))]
    public ICollection<ProfessorReview> RelatedProfessorReviews { get; set; } = new List<ProfessorReview>();
}