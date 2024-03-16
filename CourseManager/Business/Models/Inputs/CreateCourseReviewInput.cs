namespace Business.Models.Inputs;

public class CreateCourseReviewInput
{
    public bool IsLiked { get; set; }
    public int EasyRating { get; set; }
    public int UsefulRating { get; set; }
    public string? ReviewText { get; set; }
    public int? ProfessorId { get; set; }
}