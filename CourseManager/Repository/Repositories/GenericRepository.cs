using System.Collections.ObjectModel;
using System.Linq.Expressions;
using Data;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Repositories.Interfaces;

namespace Repositories.Repositories;

public abstract class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class, IEntity
{
    private readonly IDbContextFactory<CourseManagerDbContext> _dbContextFactory;

    protected GenericRepository(IDbContextFactory<CourseManagerDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }
    
    public async Task InsertAsync(TEntity entity)
    {
        using (var dbContext = await _dbContextFactory.CreateDbContextAsync())
        {
           await dbContext.AddAsync(entity);
           await dbContext.SaveChangesAsync();
        }
    }

    public async Task UpdateAsync(TEntity entity)
    {
        using (var dbContext = await _dbContextFactory.CreateDbContextAsync())
        {
            dbContext.Update(entity);
            await dbContext.SaveChangesAsync();
        }
    }

    public async Task DeleteAsync(int id)
    {
        using (var dbContext = await _dbContextFactory.CreateDbContextAsync())
        {
            var entity = await dbContext.Set<TEntity>().FirstOrDefaultAsync(e => e.Id == id);
            if (entity != null)
            {
                dbContext.Remove(entity);
                await dbContext.SaveChangesAsync();
            }
        }   
    }

    public async Task<TEntity?> GetSingleOrDefaultAsync(Expression<Func<TEntity, bool>> expression)
    {
        using (var dbContext = await _dbContextFactory.CreateDbContextAsync())
        {
            var entity = await dbContext.Set<TEntity>().FirstOrDefaultAsync(expression);
            return entity;
        }
    }

    public async Task<TEntity> GetByIdAsync(int id)
    {
        using (var dbContext = await _dbContextFactory.CreateDbContextAsync())
        {
            var entity = await dbContext.Set<TEntity>().FirstOrDefaultAsync(e => e.Id == id);
            
            if (entity == null)
            {
                throw new Exception($"Entity with id {id} not found");
            }
            return entity;
        }
    }

    public async Task<IReadOnlyCollection<TEntity>> GetByConditionAsync(Expression<Func<TEntity, bool>> expression)
    {
        using (var dbContext = await _dbContextFactory.CreateDbContextAsync())
        {
            var entities = await dbContext.Set<TEntity>().Where(expression).ToListAsync();
            return new ReadOnlyCollection<TEntity>(entities);
        }
    }

    public async Task<IReadOnlyCollection<TEntity>> GetAllAsync()
    {
        using (var dbContext = await _dbContextFactory.CreateDbContextAsync())
        {
            var entities = await dbContext.Set<TEntity>().ToListAsync();
            return new ReadOnlyCollection<TEntity>(entities);
        }
    }
}