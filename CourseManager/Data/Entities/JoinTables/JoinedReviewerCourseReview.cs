using System.ComponentModel.DataAnnotations;
using Data.Interfaces;

namespace Data.Entities.JoinTables;

public class JoinedReviewerCourseReview : IEntity
{
    public int CourseReviewId { get; set; }
    public CourseReview CourseReview { get; set; } = null!;

    public int ReviewerId { get; set; }
    public Reviewer Reviewer { get; set; } = null!;

    [Key] public int Id { get; set; }
}