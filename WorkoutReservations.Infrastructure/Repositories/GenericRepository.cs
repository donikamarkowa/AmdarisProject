
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace WorkoutReservations.Infrastructure.Repositories
{
    public class GenericRepository<TEntity, TContext> : IGenericRepository<TEntity, TContext>
        where TEntity : class
        where TContext : DbContext
    {
        protected readonly TContext _dbContext;
        protected readonly DbSet<TEntity> _set;

        public GenericRepository(TContext context)
        {
            this._dbContext = context;
            _set = context.Set<TEntity>();
        }
        public async Task Add(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            await _set.AddAsync(entity);
        }

        public void Delete(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _set.Remove(entity);
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _set.ToListAsync();
        }

        public Task<TEntity> GetByWithInclude(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var query = IncludeProperties(includeProperties).Where(predicate);
            return query.FirstOrDefaultAsync()!;
        }

        public async Task<IEnumerable<TEntity>> GetAllBy(Expression<Func<TEntity, bool>> predicate)
        {
            return await _set.Where(predicate).ToListAsync();
        }
        public async Task<TEntity?> GetById(Guid id)
        {
            return await _set.FindAsync(id);
        }
        public void Update(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _set.Update(entity);
        }
        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _set.AnyAsync(predicate);
        }
        public async Task<IEnumerable<string>> GetPropertyValuesWithIncludeAsync(Expression<Func<TEntity, string>> selector, Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var query = IncludeProperties(includeProperties).Where(predicate);
            return await query.Select(selector).ToListAsync();
        }


        #region private methods
        private IQueryable<TEntity> IncludeProperties(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> entities = _dbContext.Set<TEntity>();
            foreach (var includeProperty in includeProperties)
            {
                entities = entities.Include(includeProperty);
            }
            return entities;
        }
        #endregion
    }

}
