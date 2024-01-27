using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Data.Entities.JoinTables;
using Data.Interfaces;

namespace Data.Entities;

public class SectionLocationAndTime : IEntity
{
    [Key]
    public int Id { get; set; }

    public IEnumerable<JoinedDowSlt>? DaysOfWeek { get; set; } = null!;
    
    [StringLength(30)]
    public string Location { get; set; } = string.Empty;
    [StringLength(30)]
    public string Time { get; set; } = String.Empty; 
    
    public int SectionId { get; set; }
    [ForeignKey(nameof(SectionId))]
    public Section Section { get; set; } = null!;
}