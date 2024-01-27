using Data.Entities.EnumTables;

namespace Data.Entities.JoinTables;

public class JoinedDowSlt
{
    public int DowId { get; set; }
    public DayOfWeekEnumEntity Dow { get; set; } = null!;
    
    public int SltId { get; set; }  
    public SectionLocationAndTime Slt { get; set; } = null!;
}