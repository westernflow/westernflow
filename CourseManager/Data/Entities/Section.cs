using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Data.Entities.JoinTables;
using Data.Enums;
using Data.Interfaces;

namespace Data.Entities;

public record SectionConstructorParams
{
    public int Number { get; init; }
    public ComponentType ComponentType { get; init; }
    public int ClassNumber { get; init; }
    public string? TimetableRequisiteString { get; init; }
    public int WaitListSize { get; init; }
    public StatusType Status { get; init; }
    public Campus Campus { get; init; }
    public DeliveryType Delivery { get; init; }
    public List<string> ProfessorNames { get; init; } = new();
    public List<TimingDetails> TimingDetails { get; init; } = new();
    public int CourseOfferingId { get; init; }
}

public class Section :IEntity
{
    // see https://stackoverflow.com/questions/54400115/no-suitable-constructor-found-for-entity-type-string
    private Section()
    {
        TimingDetails = new List<TimingDetails>();
        ProfessorNames = new List<string>();
    }
    
    public Section([NotNull] SectionConstructorParams sectionConstructorParams)
    {
        Number = sectionConstructorParams.Number;
        ComponentType = sectionConstructorParams.ComponentType;
        ClassNumber = sectionConstructorParams.ClassNumber;
        TimetableRequisiteString = sectionConstructorParams.TimetableRequisiteString;
        WaitListSize = sectionConstructorParams.WaitListSize;
        Status = sectionConstructorParams.Status;
        Campus = sectionConstructorParams.Campus;
        Delivery = sectionConstructorParams.Delivery;
        ProfessorNames = sectionConstructorParams.ProfessorNames;
        TimingDetails = sectionConstructorParams.TimingDetails;
        CourseOfferingId = sectionConstructorParams.CourseOfferingId;
    }

    [Key]
    public int Id { get; set; }
    
    public int Number { get; set; }
    public ComponentType ComponentType { get; set; }
    public int ClassNumber { get; set; }
    [StringLength(5000)]
    public string? TimetableRequisiteString { get; set; }
    public int WaitListSize { get; set; }
    public StatusType Status { get; set; }
    public Campus Campus { get; set; }
    public DeliveryType Delivery { get; set; }
    public List<string> ProfessorNames { get; set; } 
    
    [InverseProperty(nameof(Entities.TimingDetails.Section))] 
    public List<TimingDetails> TimingDetails { get; set; } 
    
    public int CourseOfferingId { get; set; }
    [ForeignKey(nameof(CourseOfferingId))]
    public CourseOffering CourseOffering { get; set; } = null!;
    
    // An attempt will be made to resolve this field via the listed instructors on RMP
    public IEnumerable<JoinedSectionProfessor> Professors { get; set; } = null!;
}