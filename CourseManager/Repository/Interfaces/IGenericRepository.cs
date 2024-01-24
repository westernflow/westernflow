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
     }
}