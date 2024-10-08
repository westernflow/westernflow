using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Data.Interfaces;

namespace Data.Entities;

public class ProfessorReview : IEntity, ITrackCreated, ITrackModified
{
    // see https://stackoverflow.com/questions/54400115/no-suitable-constructor-found-for-entity-type-string
    private ProfessorReview()
    {
    }

    public int Quality { get; set; }
    public int Difficulty { get; set; }
    public int Helpful { get; set; }
    public int Clarity { get; set; }

    [StringLength(1000)] public string ReviewText { get; set; } = string.Empty;

    public int ReviewerId { get; set; }

    [ForeignKey(nameof(ReviewerId))]
    [InverseProperty("ProfessorReviewsWritten")]
    public virtual Reviewer Reviewer { get; set; } = null!;

    public int? CourseId { get; set; }

    [ForeignKey(nameof(CourseId))]
    [InverseProperty("RelatedProfessorReviews")]
    public Course? Course { get; set; }

    public int ProfessorId { get; set; }

    [ForeignKey(nameof(ProfessorId))]
    [InverseProperty("ProfessorReviews")]
    public Professor Professor { get; set; } = null!;
    
    public List<Reviewer> LikedBy { get; set; } = new List<Reviewer>();
    
    [Key] public int Id { get; set; }

    public DateTime CreatedDate { get; set; }
    public DateTime ModifiedDate { get; set; }
}