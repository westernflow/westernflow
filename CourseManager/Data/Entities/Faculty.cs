using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Data.Interfaces;

namespace Data.Entities;

public class Faculty : IEntity
{
   [Key]
   public int Id { get; set; }
   
   [StringLength(3)]
   public string? EnumBitmap { get; set; } = string.Empty;
   
   [StringLength(100)]
   public string Name { get; set; } = string.Empty;

    public Faculty(string facultyName, string abbreviation)
    {
        Name = facultyName;
        Abbreviation = abbreviation;
    }

   public Faculty()
   {
   }
}