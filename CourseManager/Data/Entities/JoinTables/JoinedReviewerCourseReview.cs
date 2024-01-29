using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Data.Interfaces;

namespace Data.Entities.JoinTables;

public class JoinedReviewerCourseReview : IEntity
{
    [Key]
    public int Id { get; set; }
    
    public int CourseReviewId { get; set; }
    public CourseReview CourseReview { get; set; } = null!;
    
    public int ReviewerId { get; set; }
    public Reviewer Reviewer { get; set; } = null!;
}