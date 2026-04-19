namespace YogoPop.DB.EFCore;

public abstract partial class EFDBContext : IDBContext
{
    public virtual int Count<TEntity>(Expression<Func<TEntity, bool>> expression, bool filterDelete = true) where TEntity : class, IDBEntity, new()
    {
        return GetQueryable<TEntity>(filterDelete).Where(expression).Count();
    }

    public virtual async Task<int> CountAsync<TEntity>(Expression<Func<TEntity, bool>> expression, bool filterDelete = true) where TEntity : class, IDBEntity, new()
    {
        return await GetQueryable<TEntity>(filterDelete).Where(expression).CountAsync();
    }

    public virtual async Task<int> CountAsync<TEntity>(CancellationToken cancellationToken, Expression<Func<TEntity, bool>> expression, bool filterDelete = true) where TEntity : class, IDBEntity, new()
    {
        return await GetQueryable<TEntity>(filterDelete).Where(expression).CountAsync(cancellationToken);
    }
}