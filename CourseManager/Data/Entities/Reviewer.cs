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
    public virtual ICollection<CourseReview> CourseReviewsWritten { get; set; } = new List<CourseReview>();

    [InverseProperty("Reviewer")]
    public virtual ICollection<ProfessorReview> ProfessorReviewsWritten { get; set; } = new List<ProfessorReview>();

    public virtual IEnumerable<JoinedReviewerCourseReview>? CourseReviewsLiked { get; set; } =
        new List<JoinedReviewerCourseReview>();
    
    public virtual IEnumerable<JoinedReviewerProfessorReview>? ProfessorReviewsLiked { get; set; } = new List<JoinedReviewerProfessorReview>();
}