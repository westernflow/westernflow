using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Data.Entities.JoinTables;
using Data.Interfaces;

namespace Data.Entities;

public class Reviewer : IEntity
{
    [Key]
    public int Id { get; set; }

    [MaxLength(255)]
    public string SubjectIdentifier { get; set; }

    [InverseProperty("Reviewer")]
    public virtual ICollection<CourseReview> CourseReviewsWritten { get; set; } = new List<CourseReview>();

    [InverseProperty("Reviewer")]
    public virtual ICollection<ProfessorReview> ProfessorReviewsWritten { get; set; } = new List<ProfessorReview>();

    public virtual IEnumerable<JoinedReviewerCourseReview>? CourseReviewsLiked { get; set; } =
        new List<JoinedReviewerCourseReview>();
    
    public virtual IEnumerable<JoinedReviewerProfessorReview>? ProfessorReviewsLiked { get; set; } = new List<JoinedReviewerProfessorReview>();
}