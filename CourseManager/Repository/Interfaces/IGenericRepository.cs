using System.Linq.Expressions;
using Data;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Repositories.Interfaces;

public interface IGenericRepository<TEntity> where TEntity : class, IEntity
{
    IQueryable<TEntity> GetQueryable();
    Task InsertAsync(TEntity entity);
    Task InsertRangeAsync(IReadOnlyCollection<TEntity> entities);
    Task UpdateAsync(TEntity entity);

    Task UpdateAsync(TEntity entity, CourseManagerDbContext dbContext, EntityEntry<TEntity> contextEntity,
        params Expression<Func<TEntity, object>>[] properties);

    Task DeleteAsync(int id);
    Task<TEntity?> GetSingleOrDefaultAsync(Expression<Func<TEntity, bool>> expression);
    Task<TEntity> GetByIdAsync(int id);
    Task<IReadOnlyCollection<TEntity>> GetByConditionAsync(Expression<Func<TEntity, bool>> expression);
    Task<IReadOnlyCollection<TEntity>> GetAllAsync();
}