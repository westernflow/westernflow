using System.ComponentModel.DataAnnotations;
using Data.Entities.JoinTables;
using Data.Interfaces;

namespace Data.Entities.EnumTables;

public class DayOfWeekEnumEntity : IEntity
{
    [Key]
    public int Id { get; set; }

    public DayOfWeek DayOfWeek { get; set; }
    [StringLength(10)]
    public string DayOfWeekString { get; set; } = String.Empty;
    
    public IEnumerable<JoinedDowSlt>? SectionLocationAndTimes { get; set; } = null!;
}