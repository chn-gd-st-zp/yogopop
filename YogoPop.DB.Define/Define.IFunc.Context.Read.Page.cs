namespace YogoPop.DB.Define;

public partial interface IDBContextFunc
{
    public Tuple<List<TEntity>, int> Page<TEntity, TSort>(Expression<Func<TEntity, bool>> expression = null, IDTOPager<TSort> param = null, bool filterDelete = true) where TEntity : class, IDBEntity, new() where TSort : IDTOSort, new();

    public Task<Tuple<List<TEntity>, int>> PageAsync<TEntity, TSort>(Expression<Func<TEntity, bool>> expression = null, IDTOPager<TSort> param = null, bool filterDelete = true) where TEntity : class, IDBEntity, new() where TSort : IDTOSort, new();

    public Task<Tuple<List<TEntity>, int>> PageAsync<TEntity, TSort>(CancellationToken cancellationToken, Expression<Func<TEntity, bool>> expression = null, IDTOPager<TSort> param = null, bool filterDelete = true) where TEntity : class, IDBEntity, new() where TSort : IDTOSort, new();


    public Tuple<List<TEntity>, int> PageByQueryable<TEntity, TSort>(object queryObj, IDTOPager<TSort> param = null, bool filterDelete = true) where TEntity : class, IDBEntity, new() where TSort : IDTOSort, new();

    public Task<Tuple<List<TEntity>, int>> PageByQueryableAsync<TEntity, TSort>(object queryObj, IDTOPager<TSort> param = null, bool filterDelete = true) where TEntity : class, IDBEntity, new() where TSort : IDTOSort, new();

    public Task<Tuple<List<TEntity>, int>> PageByQueryableAsync<TEntity, TSort>(CancellationToken cancellationToken, object queryObj, IDTOPager<TSort> param = null, bool filterDelete = true) where TEntity : class, IDBEntity, new() where TSort : IDTOSort, new();
}