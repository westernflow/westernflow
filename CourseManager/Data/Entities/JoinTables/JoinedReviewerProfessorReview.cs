namespace Data.Entities.JoinTables;

public class JoinedReviewerProfessorReview
{
    public int ProfessorReviewId { get; set; }
    public ProfessorReview ProfessorReview { get; set; } = null!;
    
    public int ReviewerId { get; set; }
    public Reviewer Reviewer { get; set; } = null!;
}