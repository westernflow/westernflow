using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Data.Interfaces;

namespace Data.Entities;

public class Professor : IEntity
{
    [Key] public int Id { get; set; }
    
    [StringLength(100)]
    public string Name { get; set; }
    [StringLength(100)]
    public string RmpId { get; set; }

    [InverseProperty("Professor")]
    public virtual ICollection<ProfessorReview>? Reviews { get; set; }
    
    [InverseProperty("Professors")]
    public virtual ICollection<Section>? Sections { get; set; }

    private Professor()
    {
    }

    public Professor(string name, string rmpId="")
    {
        Name = name;
        RmpId = rmpId;
    }
}