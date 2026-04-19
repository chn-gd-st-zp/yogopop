namespace YogoPop.DB.Define;

public abstract partial class DBRepository<TEntity>
{
    public virtual int Count(Expression<Func<TEntity, bool>> expression, bool filterDelete = true)
    {
        return DBContext.Count(expression, filterDelete);
    }

    public virtual async Task<int> CountAsync(Expression<Func<TEntity, bool>> expression, bool filterDelete = true)
    {
        return await DBContext.CountAsync(expression, filterDelete);
    }

    public virtual async Task<int> CountAsync(CancellationToken cancellationToken, Expression<Func<TEntity, bool>> expression, bool filterDelete = true)
    {
        return await DBContext.CountAsync(cancellationToken, expression, filterDelete);
    }
}