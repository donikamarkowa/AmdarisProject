using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace WorkoutReservations.Infrastructure.Repositories
{
    public interface IGenericRepository<TEntity, TContext>
        where TEntity : class
        where TContext : DbContext
    {
        Task<TEntity> GetById(Guid id);
        Task<TEntity> GetByWithInclude(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties);
        Task<IEnumerable<TEntity>> GetAll();
        Task<IEnumerable<TEntity>> GetAllBy(Expression<Func<TEntity, bool>> predicate);
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);
        Task<IEnumerable<string>> GetPropertyValuesAsync(Expression<Func<TEntity, string>> propertySelector, Expression<Func<TEntity, bool>> propertyPredicate);
        Task Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }

}
