using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TddExample.Data.Context;
using TddExample.Data.Interface;
using TddExample.Domain;

namespace TddExample.Data.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        protected readonly TddExampleContext _context;
        public Repository(TddExampleContext context)
        {
            _context = context;
        }

        public virtual async Task AddAsync(TEntity entity)
        {
            entity.CreatedIn = DateTime.UtcNow;
            await _context.Set<TEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(Guid id)
        {
            var entity = await GetByIdAsync(id);
            entity.IsDeleted = true;
            await _context.SaveChangesAsync();
        }

        public async Task<IList<TEntity>> GetAllAsync()
        {
            return await _context.Set<TEntity>().AsNoTracking().ToListAsync();
        }

        public async Task<TEntity?> GetByIdAsync(Guid id)
        {
            return await _context.Set<TEntity>().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IList<TEntity>> GetItemAsync(Expression<Func<TEntity, bool>> wherePredicate)
        {
            return await _context.Set<TEntity>().Where(wherePredicate).ToListAsync();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            entity.UpdatedIn = DateTime.UtcNow;
            _context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}

