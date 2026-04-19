namespace YogoPop.DB.Define;

public partial interface IDBRepositoryFunc<TEntity>
{
    public Tuple<List<TEntity>, int> Page<TSort>(Expression<Func<TEntity, bool>> expression = null, IDTOPager<TSort> param = null, bool filterDelete = true) where TSort : IDTOSort, new();

    public Task<Tuple<List<TEntity>, int>> PageAsync<TSort>(Expression<Func<TEntity, bool>> expression = null, IDTOPager<TSort> param = null, bool filterDelete = true) where TSort : IDTOSort, new();

    public Task<Tuple<List<TEntity>, int>> PageAsync<TSort>(CancellationToken cancellationToken, Expression<Func<TEntity, bool>> expression = null, IDTOPager<TSort> param = null, bool filterDelete = true) where TSort : IDTOSort, new();
}