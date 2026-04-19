namespace YogoPop.DB.EFCore;

public abstract partial class EFDBContext : IDBContext
{
    public virtual bool Any<TEntity>(Expression<Func<TEntity, bool>> expression, bool filterDelete = true) where TEntity : class, IDBEntity, new()
    {
        return GetQueryable<TEntity>(filterDelete).Where(expression).Any();
    }

    public virtual async Task<bool> AnyAsync<TEntity>(Expression<Func<TEntity, bool>> expression, bool filterDelete = true) where TEntity : class, IDBEntity, new()
    {
        return await GetQueryable<TEntity>(filterDelete).Where(expression).AnyAsync();
    }

    public virtual async Task<bool> AnyAsync<TEntity>(CancellationToken cancellationToken, Expression<Func<TEntity, bool>> expression, bool filterDelete = true) where TEntity : class, IDBEntity, new()
    {
        return await GetQueryable<TEntity>(filterDelete).Where(expression).AnyAsync(cancellationToken);
    }
}