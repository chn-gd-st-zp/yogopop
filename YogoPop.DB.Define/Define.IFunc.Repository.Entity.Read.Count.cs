namespace YogoPop.DB.Define;

public partial interface IDBRepositoryFunc<TEntity>
{
    public int Count(Expression<Func<TEntity, bool>> expression, bool filterDelete = true);

    public Task<int> CountAsync(Expression<Func<TEntity, bool>> expression, bool filterDelete = true);

    public Task<int> CountAsync(CancellationToken cancellationToken, Expression<Func<TEntity, bool>> expression, bool filterDelete = true);
}