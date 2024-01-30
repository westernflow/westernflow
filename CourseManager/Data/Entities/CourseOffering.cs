using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Data.Enums;
using Data.Interfaces;

namespace Data.Entities;

public class CourseOffering : IEntity
{
    private CourseOffering()
    {
    }
    
    public CourseOffering(int year, Suffix suffix, int courseId)
    {
        Year = year;
        Suffix = suffix;
        CourseId = courseId;
        
        Sections = new List<Section>();
    }
    
    [Key]
    public int Id { get; set; }  
    
    public int Year { get; set; }
    
    public Suffix Suffix { get; set; }
    
    public int CourseId { get; set; }
    [ForeignKey(nameof(CourseId))]
    public Course Course { get; set; } = null!;
    
    [InverseProperty(nameof(Section.CourseOffering))]
    public IEnumerable<Section> Sections { get; set; } = null!;
}