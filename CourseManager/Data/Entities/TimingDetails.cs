using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Data.Interfaces;

namespace Data.Entities;

public class TimingDetails : IEntity
{
    // s[i] = 1 => that day of the week is included so s[0]=1 => Sunday is included.. e.g. 0110000 => Monday and Tuesday
    [StringLength(7)] public string DaysOfWeekBitmap { get; set; } = null!;

    [StringLength(30)] public string Location { get; set; } = string.Empty;

    [StringLength(30)] public string Time { get; set; } = string.Empty;

    public int SectionId { get; set; }

    [ForeignKey(nameof(SectionId))] public Section Section { get; set; } = null!;

    [Key] public int Id { get; set; }
}