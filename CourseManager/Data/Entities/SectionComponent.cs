using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
    [StringLength(1000)]
    public string? TimetableRequisiteString { get; set; }
    [StringLength(1000)]
    public string? PrerequisiteString { get; set; }
    [StringLength(1000)]
    public string? CorequisiteString { get; set; }
    [StringLength(1000)]
    public string? AntirequisiteString { get; set; }
    [StringLength(100)]
    public string? Status { get; set; }
    [StringLength(100)]
    public string? Campus { get; set; }
    [StringLength(100)]
    public string? Delivery { get; set; }
    
    public int CourseId { get; set; }
    [ForeignKey(nameof(CourseId))]
    public virtual Course? Course { get; set; }
    
    public virtual ICollection<Professor>? Professors { get; set; }

}