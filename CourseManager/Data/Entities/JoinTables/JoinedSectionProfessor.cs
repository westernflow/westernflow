namespace Data.Entities.JoinTables;

public class JoinedSectionProfessor
{
    public int SectionId { get; set; }
    public Section Section { get; set; } = null!;
    
    public int ProfessorId { get; set; }
    public Professor Professor { get; set; } = null!;
}