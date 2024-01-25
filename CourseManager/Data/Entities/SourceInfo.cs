using System.ComponentModel.DataAnnotations;
using Data.Interfaces;

namespace Data.Entities;

public class SourceInfo : IEntity
{
    [Key]
    public int Id { get; set; }
    [StringLength(2000)]
    public string Title { get; set; }
    [StringLength(10)]
    public string Year { get; set; }
    [StringLength(2000)]
    public string Url { get; set; }

    private SourceInfo() { }

    public SourceInfo(string title, string year, string url)
    {
        Title = title;
        Year = year;
        Url = url;
    }
}