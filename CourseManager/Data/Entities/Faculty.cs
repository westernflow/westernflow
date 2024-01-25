using System.ComponentModel.DataAnnotations;
using Data.Interfaces;

namespace Data.Entities;

public class Faculty : IEntity
{
   [Key]
   public int Id { get; set; }
   [StringLength(100)]
   public string FacultyName { get; set; } = string.Empty;

   private Faculty()
   {
   }

   public Faculty(string facultyName)
   {
      FacultyName = facultyName;
   }
}