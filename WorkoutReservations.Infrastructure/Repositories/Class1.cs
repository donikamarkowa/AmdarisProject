using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

public abstract class BaseRepository<TModel, TContext> //: IEFBaseRepository<TModel>
    where TModel : class
    where TContext : DbContext
{
    protected readonly TContext context;
    protected readonly DbSet<TModel> set;

    public BaseRepository(TContext context)
    {
        this.context = context;
        set = context.Set<TModel>();
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await context.SaveChangesAsync(cancellationToken);
    }

    public void ClearChangeTracker()
    {
        context.ChangeTracker.Clear();
    }

    public void Add(TModel entity)
    {
        set.Add(entity);
    }

    public void AddRange(IEnumerable<TModel> models)
    {
        set.AddRange(models);
    }

    public async Task<bool> AnyAsync(Expression<Func<TModel, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await set.AnyAsync(predicate, cancellationToken);
    }

    public async Task<TModel?> FindByAsync(Expression<Func<TModel, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await set.Where(predicate).FirstOrDefaultAsync(cancellationToken);
    }
    public async Task<TModel?> FindByWithIncludeAsync(Expression<Func<TModel, bool>> predicate, CancellationToken cancellationToken, params Expression<Func<TModel, object>>[] includeProperties)
    {
        var query = IncludeProperties(includeProperties);
        return await query.FirstOrDefaultAsync(predicate, cancellationToken);
    }

    public async Task<IList<TModel>> FindAllByWithIncludeAsync(Expression<Func<TModel, bool>> predicate, CancellationToken cancellationToken, params Expression<Func<TModel, object>>[] includeProperties)
    {
        var query = IncludeProperties(includeProperties);
        return await query.Where(predicate).ToListAsync(cancellationToken);
    }

    public async Task<IList<TModel>> FindAllWithIncludeAsync(CancellationToken cancellationToken, params Expression<Func<TModel, object>>[] includeProperties)
    {
        var query = IncludeProperties(includeProperties);
        return await query.ToListAsync(cancellationToken);
    }

    public async Task<IList<TModel>> FindAllByAsync(Expression<Func<TModel, bool>> predicate, CancellationToken cancellationToken)
    {
        return await set.Where(predicate).ToListAsync(cancellationToken);
    }

    public async Task<IList<TModel>> ListAsync(CancellationToken cancellationToken)
    {
        return await set.ToListAsync(cancellationToken);
    }

    public async Task<IList<TModel>> ListAsync(Expression<Func<TModel, bool>> predicate, CancellationToken cancellationToken)
    {
        var result = await set.Where(predicate)
            .ToListAsync(cancellationToken);
        return result;
    }

    private IQueryable<TModel> IncludeProperties(params Expression<Func<TModel, object>>[] includeProperties)
    {
        IQueryable<TModel> entities = set;
        foreach (var includeProperty in includeProperties)
        {
            entities = entities.Include(includeProperty);
        }
        return entities;
    }

    public TModel Update([NotNull] TModel entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        return set.Update(entity).Entity;
    }

    public void UpdateRange(IEnumerable<TModel> models)
    {
        set.UpdateRange(models);
    }

    public void Remove(TModel entity)
    {
        set.Remove(entity);
    }
}