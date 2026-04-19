namespace YogoPop.DB.Define;

public partial interface IDBRepositoryFunc
{
    public List<TEntity> ListByQueryable<TEntity, TSort>(object queryObj, IDTOListor<TSort> param = null, bool filterDelete = true) where TEntity : class, IDBEntity, new() where TSort : IDTOSort, new();

    public Task<List<TEntity>> ListByQueryableAsync<TEntity, TSort>(object queryObj, IDTOListor<TSort> param = null, bool filterDelete = true) where TEntity : class, IDBEntity, new() where TSort : IDTOSort, new();

    public Task<List<TEntity>> ListByQueryableAsync<TEntity, TSort>(CancellationToken cancellationToken, object queryObj, IDTOListor<TSort> param = null, bool filterDelete = true) where TEntity : class, IDBEntity, new() where TSort : IDTOSort, new();
}