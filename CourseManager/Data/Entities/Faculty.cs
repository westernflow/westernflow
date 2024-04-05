using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Entities;

public class Faculty : IEntity
{
   [Key]
   public int Id { get; set; }
   
   [StringLength(3)]
   public string? EnumBitmap { get; set; } = string.Empty;
   
   [StringLength(100)]
   public string Name { get; set; } = string.Empty;

   [StringLength(20)] public string Abbreviation { get; set; } = string.Empty;
   
   [InverseProperty(nameof(Course.Faculty))]
   public virtual IEnumerable<Course> Courses { get; set; } = new List<Course>();

   public Faculty()
   {
   }
}