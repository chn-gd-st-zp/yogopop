namespace YogoPop.DB.Define;

public abstract partial class DBRepository
{
    public virtual bool Any<TEntity>(Expression<Func<TEntity, bool>> expression, bool filterDelete = true) where TEntity : class, IDBEntity, new()
    {
        return DBContext.Any(expression, filterDelete);
    }

    public virtual async Task<bool> AnyAsync<TEntity>(Expression<Func<TEntity, bool>> expression, bool filterDelete = true) where TEntity : class, IDBEntity, new()
    {
        return await DBContext.AnyAsync(expression, filterDelete);
    }

    public virtual async Task<bool> AnyAsync<TEntity>(CancellationToken cancellationToken, Expression<Func<TEntity, bool>> expression, bool filterDelete = true) where TEntity : class, IDBEntity, new()
    {
        return await DBContext.AnyAsync(cancellationToken, expression, filterDelete);
    }
}