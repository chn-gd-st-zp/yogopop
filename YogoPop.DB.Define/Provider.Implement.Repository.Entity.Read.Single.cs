namespace YogoPop.DB.Define;

public abstract partial class DBRepository<TEntity>
{
    public virtual TEntity Single(Expression<Func<TEntity, bool>> expression, bool filterDelete = true)
    {
        return DBContext.Single(expression, filterDelete);
    }

    public virtual async Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> expression, bool filterDelete = true)
    {
        return await DBContext.SingleAsync(expression, filterDelete);
    }

    public virtual async Task<TEntity> SingleAsync(CancellationToken cancellationToken, Expression<Func<TEntity, bool>> expression, bool filterDelete = true)
    {
        return await DBContext.SingleAsync(cancellationToken, expression, filterDelete);
    }
}