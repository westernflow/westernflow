using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Data.Interfaces;

namespace Data.Entities;

public class ProfessorReview : IEntity
{
    // see https://stackoverflow.com/questions/54400115/no-suitable-constructor-found-for-entity-type-string
    private ProfessorReview()
    {
    }

    [Key]
    public int Id { get; set; }
    
    public int Quality { get; set; }
    public int Difficulty { get; set; }
    public DateTime Date { get; set; } 
    [StringLength(500)]
    public string ReviewText { get; set; } = String.Empty;
    public int Helpful { get; set; }
    public int Clarity { get; set; }

    public int ReviewerId { get; set; }
    [ForeignKey(nameof(ReviewerId))]
    [InverseProperty("ProfessorReviewsWritten")]
    public virtual Reviewer Reviewer { get; set; } = null!;
    
    public int CourseId { get; set; }
    [ForeignKey(nameof(CourseId))]
    [InverseProperty("RelatedProfessorReviews")]
    public Course Course { get; set; } = null!;
    
    public int ProfessorId { get; set; }
    [ForeignKey(nameof(ProfessorId))]
    [InverseProperty("Reviews")]
    public Professor Professor { get; set; } = null!;

    [InverseProperty("ProfessorReviewsLiked")]
    public virtual ICollection<Reviewer> LikedBy { get; set; } = null!;
}