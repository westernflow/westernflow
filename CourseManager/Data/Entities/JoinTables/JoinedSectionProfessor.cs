using System.ComponentModel.DataAnnotations;
using Data.Interfaces;

namespace Data.Entities.JoinTables;

public class JoinedSectionProfessor : IEntity
{
    public int SectionId { get; set; }
    public Section Section { get; set; } = null!;

    public int ProfessorId { get; set; }
    public Professor Professor { get; set; } = null!;

    [Key] public int Id { get; set; }
}