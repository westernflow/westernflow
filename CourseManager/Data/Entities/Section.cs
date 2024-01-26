using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Data.Interfaces;

namespace Data.Entities;



public class Section :IEntity
{
    // see https://stackoverflow.com/questions/54400115/no-suitable-constructor-found-for-entity-type-string
    private Section()
    {
        SectionLocationAndTimes = new HashSet<SectionLocationAndTime>();
        ProfessorNames = new List<string>();
    }

    [Key]
    public int Id { get; set; }
    
    public int Number { get; set; }
    public int ComponentNumber { get; set; }
    public int ClassNumber { get; set; }
    // this is likely redundant since we already have requisite information from other data source...
    [StringLength(1000)]
    public string? SectionRequisiteString { get; set; }
    [StringLength(1000)]
    public string? TimetableRequisiteString { get; set; }
    [StringLength(100)]
    public string? Status { get; set; }
    public int Waitlist { get; set; }
    [StringLength(100)]
    public string? Campus { get; set; }
    [StringLength(100)]
    public string? Delivery { get; set; }
    public List<string> ProfessorNames { get; set; } 
    
    [InverseProperty(nameof(SectionLocationAndTime.Section))] 
    public HashSet<SectionLocationAndTime> SectionLocationAndTimes { get; set; } 
    
    public int CourseId { get; set; }

    [ForeignKey(nameof(CourseId))]
    [InverseProperty("Sections")]
    public virtual Course Course { get; set; } = null!;
    
    // An attempt will be made to resolve this field via the listed instructors on RMP
    [InverseProperty("Sections")]
    public virtual ICollection<Professor> Professors { get; set; } = null!;
}