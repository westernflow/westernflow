using System.Linq.Expressions;
using Data.Interfaces;

namespace Repositories.Interfaces
{
     public interface IGenericRepository<TEntity> where TEntity : IEntity
     {
          Task InsertAsync(TEntity entity);
          Task UpdateAsync(TEntity entity);
          Task DeleteAsync(int id);
          Task<TEntity?> GetSingleOrDefaultAsync(Expression<Func<TEntity, bool>> expression);
          Task<TEntity> GetByIdAsync(int id);
          Task<IReadOnlyCollection<TEntity>> GetByConditionAsync(Expression<Func<TEntity, bool>> expression);
          Task<IReadOnlyCollection<TEntity>> GetAllAsync();
     }
}