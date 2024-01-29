using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Data.Enums;
using Data.Interfaces;

namespace Data.Entities;

public class CourseOffering : IEntity
{
    [Key]
    public int Id { get; set; }  
    
    public int Year { get; set; }
    
    public Suffix Suffix { get; set; }
    
    public int CourseId { get; set; }
    [ForeignKey(nameof(CourseId))]
    public virtual Course Course { get; set; } = null!;
    
    [InverseProperty(nameof(Section.CourseOffering))]
    public virtual IEnumerable<Section> Sections { get; set; } = null!;
}