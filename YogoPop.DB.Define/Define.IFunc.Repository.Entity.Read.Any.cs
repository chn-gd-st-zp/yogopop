namespace YogoPop.DB.Define;

public partial interface IDBRepositoryFunc<TEntity>
{
    public bool Any(Expression<Func<TEntity, bool>> expression, bool filterDelete = true);

    public Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression, bool filterDelete = true);

    public Task<bool> AnyAsync(CancellationToken cancellationToken, Expression<Func<TEntity, bool>> expression, bool filterDelete = true);
}