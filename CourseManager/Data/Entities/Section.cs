using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Data.Interfaces;

namespace Data.Entities;

public class Section :IEntity
{
    // see https://stackoverflow.com/questions/54400115/no-suitable-constructor-found-for-entity-type-string
    private Section()
    {
    }

    [Key]
    public int Id { get; set; }
    
    public int Number { get; set; }
    public int ComponentNumber { get; set; }
    public int ClassNumber { get; set; }
    [StringLength(1000)]
    public string? TimetableRequisiteString { get; set; }
    [StringLength(100)]
    public string? Status { get; set; }
    [StringLength(100)]
    public string? Campus { get; set; }
    [StringLength(100)]
    public string? Delivery { get; set; }
    
    public int CourseId { get; set; }

    [ForeignKey(nameof(CourseId))]
    [InverseProperty("Sections")]
    public virtual Course Course { get; set; } = null!;
   
    [InverseProperty("Sections")]
    public virtual ICollection<Professor>? Professors { get; set; }
}