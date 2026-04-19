namespace YogoPop.DB.Define;

public abstract partial class DBRepository<TEntity>
{
    public virtual bool Any(Expression<Func<TEntity, bool>> expression, bool filterDelete = true)
    {
        return DBContext.Any(expression, filterDelete);
    }

    public virtual async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression, bool filterDelete = true)
    {
        return await DBContext.AnyAsync(expression, filterDelete);
    }

    public virtual async Task<bool> AnyAsync(CancellationToken cancellationToken, Expression<Func<TEntity, bool>> expression, bool filterDelete = true)
    {
        return await DBContext.AnyAsync(cancellationToken, expression, filterDelete);
    }
}