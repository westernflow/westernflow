using Data;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Repositories.Interfaces;

namespace Repositories.Repositories;

internal class ProfessorRepository : GenericRepository<Professor>, IProfessorRepository
{
    private readonly IDbContextFactory<CourseManagerDbContext> _dbContextFactory;

    public ProfessorRepository(IDbContextFactory<CourseManagerDbContext> dbContextFactory) : base(dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }
    
    public IQueryable<Professor> GetQueryable()
    {
        return _dbContextFactory.CreateDbContext().Set<Professor>().AsNoTracking();
    }
    
    public async Task AddSection(Professor entity, Section section)
    {
        await using var context = _dbContextFactory.CreateDbContext();
        var sectionInContext = await context.Sections.FirstOrDefaultAsync(s => s.Id == section.Id);
        
        if (sectionInContext == null)
        {
            throw new ArgumentException("Section not found");
        }
        
        var professor = await context.Professors
            .Include(p => p.Sections)
            .FirstOrDefaultAsync(p => p.UwoId == entity.UwoId);
        
        if (professor == null)
        {
            await context.Professors.AddAsync(entity);
        }
        
        professor = professor ?? entity;
        
        professor.Sections.Add(sectionInContext);
        await context.SaveChangesAsync();
    }
}