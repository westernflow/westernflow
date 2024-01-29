using System.ComponentModel.DataAnnotations;
using Data.Interfaces;

namespace Data.Entities.JoinTables;

public class JoinedReviewerProfessorReview : IEntity
{
    [Key]
    public int Id { get; set; }
    
    public int ProfessorReviewId { get; set; }
    public ProfessorReview ProfessorReview { get; set; } = null!;
    
    public int ReviewerId { get; set; }
    public Reviewer Reviewer { get; set; } = null!;
}