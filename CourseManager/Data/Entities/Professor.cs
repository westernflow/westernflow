using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Data.Interfaces;

namespace Data.Entities;

public class Professor : IEntity
{
    public Professor()
    {
    }

    [StringLength(100)] public string Name { get; set; } = string.Empty;
    
    [StringLength(100)] public string Email { get; set; } = string.Empty;
    
    [StringLength(100)] public string UwoId { get; set; } = string.Empty;

    [StringLength(100)] public string? RmpId { get; set; }

    [InverseProperty("Professor")]
    public virtual ICollection<ProfessorReview> ProfessorReviews { get; set; } = new List<ProfessorReview>();

    [InverseProperty("Professor")]
    public virtual ICollection<CourseReview> CourseReviews { get; set; } = new List<CourseReview>();
    
    public List<Section> Sections { get; set; } = new List<Section>();
    [Key] public int Id { get; set; }
}