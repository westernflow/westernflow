using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Data.Interfaces;

namespace Data.Entities;

public class DayOfWeekEnumEntity : IEntity
{
    [Key]
    public int Id { get; set; }

    public DayOfWeek DayOfWeek { get; set; }
    
    [InverseProperty("DaysOfWeek")]
    public ICollection<SectionLocationAndTime> SectionLocationAndTimes { get; set; } = null!;
}