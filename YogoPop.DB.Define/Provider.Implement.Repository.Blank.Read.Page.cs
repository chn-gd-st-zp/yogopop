namespace YogoPop.DB.Define;

public abstract partial class DBRepository
{
    public virtual Tuple<List<TEntity>, int> PageByQueryable<TEntity, TSort>(object queryObj, IDTOPager<TSort> param = null, bool filterDelete = true) where TEntity : class, IDBEntity, new() where TSort : IDTOSort, new()
    {
        return DBContext.PageByQueryable<TEntity, TSort>(queryObj, param, filterDelete);
    }

    public virtual async Task<Tuple<List<TEntity>, int>> PageByQueryableAsync<TEntity, TSort>(object queryObj, IDTOPager<TSort> param = null, bool filterDelete = true) where TEntity : class, IDBEntity, new() where TSort : IDTOSort, new()
    {
        return await DBContext.PageByQueryableAsync<TEntity, TSort>(queryObj, param, filterDelete);
    }

    public virtual async Task<Tuple<List<TEntity>, int>> PageByQueryableAsync<TEntity, TSort>(CancellationToken cancellationToken, object queryObj, IDTOPager<TSort> param = null, bool filterDelete = true) where TEntity : class, IDBEntity, new() where TSort : IDTOSort, new()
    {
        return await DBContext.PageByQueryableAsync<TEntity, TSort>(cancellationToken, queryObj, param, filterDelete);
    }
}