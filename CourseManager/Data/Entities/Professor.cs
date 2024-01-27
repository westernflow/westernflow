using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Data.Entities.JoinTables;
using Data.Interfaces;

namespace Data.Entities;

public class Professor : IEntity
{
    [Key] public int Id { get; set; }
    
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;
    [StringLength(100)]
    public string? RmpId { get; set; }

    [InverseProperty("Professor")]
    public virtual ICollection<ProfessorReview> ProfessorReviews { get; set; } = null!;
    
    [InverseProperty("Professor")]
    public virtual ICollection<CourseReview> CourseReviews { get; set; } = null!;
    
    public virtual IEnumerable<JoinedSectionProfessor>? Sections { get; set; }

    private Professor()
    {
    }

    public Professor(string name)
    {
        Name = name;
    }
}