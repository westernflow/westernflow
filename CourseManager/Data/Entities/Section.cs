using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Data.Entities.JoinTables;
using Data.Enums;
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
    public ComponentType ComponentType { get; set; }
    public int ClassNumber { get; set; }
    [StringLength(1000)]
    public string? TimetableRequisiteString { get; set; }
    public int WaitListSize { get; set; }
    public StatusType Status { get; set; }
    public Campus Campus { get; set; }
    public DeliveryType Delivery { get; set; }
    public List<string> ProfessorNames { get; set; } 
    
    [InverseProperty(nameof(SectionLocationAndTime.Section))] 
    public HashSet<SectionLocationAndTime> SectionLocationAndTimes { get; set; } 
    
    public int CourseId { get; set; }

    [ForeignKey(nameof(CourseId))]
    [InverseProperty("Sections")]
    public virtual Course Course { get; set; } = null!;
    
    // An attempt will be made to resolve this field via the listed instructors on RMP
    public virtual IEnumerable<JoinedSectionProfessor>? Professors { get; set; } = null!;
}