using System.ComponentModel.DataAnnotations;
using Data.Interfaces;

namespace Data.Entities;

public class ProfessorReview : IEntity
{
    [Key]
    public int Id { get; set; }
    public int Quality { get; set; }
    public int Difficulty { get; set; }
    public DateTime Date { get; set; } 
    public string? ReviewText { get; set; }
    public int Helpful { get; set; }
    public int Clarity { get; set; }

    public Professor Professor { get; set; }

    public ProfessorReview(Professor professor)
    {
        Professor = professor;
    }
}