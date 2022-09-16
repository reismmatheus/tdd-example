using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TddExample.Domain;

namespace TddExample.Data.Interface
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(Guid id);
        Task<TEntity?> GetByIdAsync(Guid id);
        Task<IList<TEntity>> GetAllAsync();
        Task<IList<TEntity>> GetItemAsync(Expression<Func<TEntity, bool>> wherePredicate);
    }
}
