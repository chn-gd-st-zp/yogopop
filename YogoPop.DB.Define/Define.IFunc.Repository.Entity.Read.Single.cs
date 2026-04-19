namespace YogoPop.DB.Define;

public partial interface IDBRepositoryFunc<TEntity>
{
    public TEntity Single(Expression<Func<TEntity, bool>> expression, bool filterDelete = true);

    public Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> expression, bool filterDelete = true);

    public Task<TEntity> SingleAsync(CancellationToken cancellationToken, Expression<Func<TEntity, bool>> expression, bool filterDelete = true);
}