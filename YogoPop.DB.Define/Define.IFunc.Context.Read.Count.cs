namespace YogoPop.DB.Define;

public partial interface IDBContextFunc
{
    public int Count<TEntity>(Expression<Func<TEntity, bool>> expression, bool filterDelete = true) where TEntity : class, IDBEntity, new();

    public Task<int> CountAsync<TEntity>(Expression<Func<TEntity, bool>> expression, bool filterDelete = true) where TEntity : class, IDBEntity, new();

    public Task<int> CountAsync<TEntity>(CancellationToken cancellationToken, Expression<Func<TEntity, bool>> expression, bool filterDelete = true) where TEntity : class, IDBEntity, new();
}