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
    public string? ReviewText { get; set; }
    public int Helpful { get; set; }
    public int Clarity { get; set; }

    public Professor Professor { get; set; }

    public ProfessorReview(Professor professor)
    {
        Professor = professor;
    }
}