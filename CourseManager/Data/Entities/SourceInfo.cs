using System.ComponentModel.DataAnnotations;
using Data.Interfaces;

namespace Data.Entities;

public class SourceInfo : IEntity
{
    [Key]
    public int Id { get; set; }
    public string Title { get; set; }
    public string Year { get; set; }
    public string Url { get; set; }

    public SourceInfo(string title, string year, string url)
    {
        Title = title;
        Year = year;
        Url = url;
    }
}