using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Data.Entities.JoinTables;
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

    public virtual IEnumerable<JoinedReviewerCourseReview>? CourseReviewsLiked { get; set; } = null!;
    
    public virtual IEnumerable<JoinedReviewerProfessorReview>? ProfessorReviewsLiked { get; set; } = null!;
}