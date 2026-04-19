namespace YogoPop.DB.Define;

public abstract partial class DBRepository
{
    public virtual int Count<TEntity>(Expression<Func<TEntity, bool>> expression, bool filterDelete = true) where TEntity : class, IDBEntity, new()
    {
        return DBContext.Count(expression, filterDelete);
    }

    public virtual async Task<int> CountAsync<TEntity>(Expression<Func<TEntity, bool>> expression, bool filterDelete = true) where TEntity : class, IDBEntity, new()
    {
        return await DBContext.CountAsync(expression, filterDelete);
    }

    public virtual async Task<int> CountAsync<TEntity>(CancellationToken cancellationToken, Expression<Func<TEntity, bool>> expression, bool filterDelete = true) where TEntity : class, IDBEntity, new()
    {
        return await DBContext.CountAsync(cancellationToken, expression, filterDelete);
    }
}