using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Data.Interfaces;

namespace Data.Entities;

public class Reviewer : IEntity
{
    [MaxLength(255)] public string SubjectId { get; set; } = string.Empty;

    [InverseProperty("Reviewer")]
    public virtual ICollection<CourseReview> CourseReviewsWritten { get; set; } = new List<CourseReview>();

    [InverseProperty("Reviewer")]
    public virtual ICollection<ProfessorReview> ProfessorReviewsWritten { get; set; } = new List<ProfessorReview>();

    public List<CourseReview> CourseReviewsLiked { get; set; } = new List<CourseReview>();
    
    public List<ProfessorReview> ProfessorReviewsLiked { get; set; } = new List<ProfessorReview>();
    
    [Key] public int Id { get; set; }
}