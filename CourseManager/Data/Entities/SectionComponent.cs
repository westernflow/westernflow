using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using Data.Interfaces;

namespace Data.Entities;

public class SectionComponent : IEntity
{
    [Key]
    public int Id { get; set; } 
    public int Number { get; set; }
    public int ComponentNumber { get; set; }
    public int ClassNumber { get; set; }
    public string? TimetableRequisiteString { get; set; }
    public string? PrerequisiteString { get; set; }
    public string? CorequisiteString { get; set; }
    public string? AntirequisiteString { get; set; }
    public string? Status { get; set; }
    public string? Campus { get; set; }
    public string? Delivery { get; set; }
    
    public virtual ICollection<Professor>? Professors { get; set; }
}