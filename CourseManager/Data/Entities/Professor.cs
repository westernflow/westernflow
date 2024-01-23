using System.ComponentModel.DataAnnotations;
using Data.Interfaces;

namespace Data.Entities;

public class Professor : IEntity
{
    [Key] public int Id { get; set; }
    public string Name { get; set; }
    public int RmpId { get; set; }

    public virtual ICollection<Course>? CurrentCourses { get; set; }
    public virtual ICollection<ProfessorReview>? Reviews { get; set; }
    public virtual ICollection<Faculty>? Faculties { get; set; }

    public Professor(string name)
    {
        Name = name;
    }
}