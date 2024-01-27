using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities.JoinTables;

public class JoinedReviewerCourseReview
{
    public int CourseReviewId { get; set; }
    public CourseReview CourseReview { get; set; } = null!;
    
    public int ReviewerId { get; set; }
    public Reviewer Reviewer { get; set; } = null!;
}