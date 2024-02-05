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
    
    public CourseOffering(int year, Suffix suffix, CalendarSource source, int courseId)
    {
        Year = year;
        Suffix = suffix;
        CourseId = courseId;
    }
    
    [Key]
    public int Id { get; set; }  
    
    // uwo's internal termId e.g. 1239 for 2023 fall/winter and 1245 for 2024 summer
    public int TermId { get; set; }
    
    public int Year { get; set; }
    
    public CalendarSource CalendarSource { get; set; }
    
    public Suffix Suffix { get; set; }
    
    public int CourseId { get; set; }
    [ForeignKey(nameof(CourseId))]
    public Course Course { get; set; } = null!;
    
    [InverseProperty(nameof(Section.CourseOffering))]
    public IEnumerable<Section> Sections { get; set; } = new List<Section>();
}