namespace YogoPop.DB.Define;

public partial interface IDBRepositoryFunc
{
    public bool Any<TEntity>(Expression<Func<TEntity, bool>> expression, bool filterDelete = true) where TEntity : class, IDBEntity, new();

    public Task<bool> AnyAsync<TEntity>(Expression<Func<TEntity, bool>> expression, bool filterDelete = true) where TEntity : class, IDBEntity, new();

    public Task<bool> AnyAsync<TEntity>(CancellationToken cancellationToken, Expression<Func<TEntity, bool>> expression, bool filterDelete = true) where TEntity : class, IDBEntity, new();
}