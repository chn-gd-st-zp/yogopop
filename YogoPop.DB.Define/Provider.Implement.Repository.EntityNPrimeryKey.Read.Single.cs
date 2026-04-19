namespace YogoPop.DB.Define;

public abstract partial class DBRepository<TEntity, TPrimaryKey>
{
    public virtual TEntity Single(TPrimaryKey pk, bool filterDelete = true)
    {
        return DBContext.Single<TEntity, TPrimaryKey>(pk, filterDelete);
    }

    public virtual async Task<TEntity> SingleAsync(TPrimaryKey pk, bool filterDelete = true)
    {
        return await DBContext.SingleAsync<TEntity, TPrimaryKey>(pk, filterDelete);
    }

    public virtual async Task<TEntity> SingleAsync(CancellationToken cancellationToken, TPrimaryKey pk, bool filterDelete = true)
    {
        return await DBContext.SingleAsync<TEntity, TPrimaryKey>(cancellationToken, pk, filterDelete);
    }
}