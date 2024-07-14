using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Data.Enums;
using Data.Interfaces;

namespace Data.Entities;

public class Section : IEntity
{
    // see https://stackoverflow.com/questions/54400115/no-suitable-constructor-found-for-entity-type-string
    public Section()
    {
    }

    public int Number { get; set; }
    public ComponentType ComponentType { get; set; }
    public int ClassNumber { get; set; }

    [StringLength(5000)] public string? TimetableRequisiteString { get; set; }

    public int WaitListSize { get; set; }
    public StatusType Status { get; set; }
    public Campus Campus { get; set; }
    public DeliveryType Delivery { get; set; }
    public List<string> ProfessorNames { get; set; } = new();

    [InverseProperty(nameof(Entities.TimingDetails.Section))]
    public List<TimingDetails> TimingDetails { get; set; } = new();

    [StringLength(100)] public string TimingDetailsText { get; set; } = string.Empty;

    public int CourseOfferingId { get; set; }

    [ForeignKey(nameof(CourseOfferingId))] public CourseOffering CourseOffering { get; set; }

    public List<Professor> Professors { get; set; } = new List<Professor>();

    [Key] public int Id { get; set; }
}