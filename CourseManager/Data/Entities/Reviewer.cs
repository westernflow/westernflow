using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Data.Interfaces;

namespace Data.Entities;

public class Reviewer : IEntity
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    public string Email { get; set; } = string.Empty;
    [StringLength(50)]
    public string FirstName { get; set; } = string.Empty;
    [StringLength(50)]
    public string LastName { get; set; } = string.Empty;

    [InverseProperty("Reviewer")]
    public virtual ICollection<CourseReview> CourseReviewsWritten { get; set; } = null!;
    
    [InverseProperty("Reviewer")]
    public virtual ICollection<ProfessorReview> ProfessorReviewsWritten { get; set; } = null!;

    [InverseProperty("LikedBy")]
    public virtual ICollection<CourseReview> CourseReviewsLiked { get; set; } = null!;
    
    [InverseProperty("LikedBy")]
    public virtual ICollection<ProfessorReview> ProfessorReviewsLiked { get; set; } = null!;
}