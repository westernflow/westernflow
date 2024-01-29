using System.ComponentModel.DataAnnotations;
using Data.Entities.EnumTables;
using Data.Interfaces;

namespace Data.Entities.JoinTables;

public class JoinedDowSlt : IEntity
{
    [Key]
    public int Id { get; set; }
    
    public int DowId { get; set; }
    public DayOfWeekEnumEntity Dow { get; set; } = null!;
    
    public int SltId { get; set; }  
    public SectionLocationAndTime Slt { get; set; } = null!;
}