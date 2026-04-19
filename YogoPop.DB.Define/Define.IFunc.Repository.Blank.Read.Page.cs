namespace YogoPop.DB.Define;

public partial interface IDBRepositoryFunc
{
    public Tuple<List<TEntity>, int> PageByQueryable<TEntity, TSort>(object queryObj, IDTOPager<TSort> param = null, bool filterDelete = true) where TEntity : class, IDBEntity, new() where TSort : IDTOSort, new();

    public Task<Tuple<List<TEntity>, int>> PageByQueryableAsync<TEntity, TSort>(object queryObj, IDTOPager<TSort> param = null, bool filterDelete = true) where TEntity : class, IDBEntity, new() where TSort : IDTOSort, new();

    public Task<Tuple<List<TEntity>, int>> PageByQueryableAsync<TEntity, TSort>(CancellationToken cancellationToken, object queryObj, IDTOPager<TSort> param = null, bool filterDelete = true) where TEntity : class, IDBEntity, new() where TSort : IDTOSort, new();
}