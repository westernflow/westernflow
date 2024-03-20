using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Data.Entities.JoinTables;
using Data.Interfaces;

namespace Data.Entities;

public class CourseReview : IEntity, ITrackCreated, ITrackModified
{
    [Key]
    public int Id { get; set; }
    
    public int ReviewerId { get; set; } 
    [ForeignKey(nameof(ReviewerId))]
    [InverseProperty("CourseReviewsWritten")]
    public virtual Reviewer Reviewer { get; set; } = null!;
    
    public int CourseId { get; set; }
    [ForeignKey(nameof(CourseId))]
    public virtual Course Course { get; set; } = null!;

    public bool IsLiked { get; set; }
    public int EasyRating { get; set; }
    public int UsefulRating { get; set; }

    [StringLength(1000)]
    public string ReviewText { get; set; } = String.Empty;
    
    public int? ProfessorId { get; set; }
    [ForeignKey(nameof(ProfessorId))]
    public virtual Professor? Professor { get; set; }

    public virtual IEnumerable<JoinedReviewerCourseReview>? LikedBy { get; set; } = new List<JoinedReviewerCourseReview>();
    public DateTime CreatedDate { get; set; }
    public DateTime ModifiedDate { get; set; }
}