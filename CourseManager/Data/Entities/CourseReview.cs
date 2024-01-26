using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Data.Interfaces;

namespace Data.Entities;

public class CourseReview : IEntity
{
    [Key]
    public int Id { get; set; }
    
    public int ReviewerId { get; set; } 
    [ForeignKey(nameof(ReviewerId))]
    [InverseProperty("CourseReviewsWritten")]
    public virtual Reviewer Reviewer { get; set; } = null!;

    public bool IsLiked { get; set; }
    public int EasyRating { get; set; }
    public int UsefulRating { get; set; }

    [StringLength(500)]
    public string ReviewText { get; set; } = String.Empty;

    [InverseProperty("CourseReviewsLiked")]
    public virtual ICollection<Reviewer> LikedBy { get; set; } = null!;

}