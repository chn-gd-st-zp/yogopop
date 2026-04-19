namespace YogoPop.DB.Define;

public partial interface IDBContextFunc
{
    public List<TEntity> List<TEntity, TPrimaryKey, TSort>(TPrimaryKey[] pks, IDTOListor<TSort> param = null, bool filterDelete = true) where TEntity : class, IDBEntity, IDBFPrimaryKey<TPrimaryKey>, new() where TSort : IDTOSort, new();

    public Task<List<TEntity>> ListAsync<TEntity, TPrimaryKey, TSort>(TPrimaryKey[] pks, IDTOListor<TSort> param = null, bool filterDelete = true) where TEntity : class, IDBEntity, IDBFPrimaryKey<TPrimaryKey>, new() where TSort : IDTOSort, new();

    public Task<List<TEntity>> ListAsync<TEntity, TPrimaryKey, TSort>(CancellationToken cancellationToken, TPrimaryKey[] pks, IDTOListor<TSort> param = null, bool filterDelete = true) where TEntity : class, IDBEntity, IDBFPrimaryKey<TPrimaryKey>, new() where TSort : IDTOSort, new();


    public List<TEntity> List<TEntity>(Expression<Func<TEntity, bool>> expression = null, bool filterDelete = true) where TEntity : class, IDBEntity, new();

    public Task<List<TEntity>> ListAsync<TEntity>(Expression<Func<TEntity, bool>> expression = null, bool filterDelete = true) where TEntity : class, IDBEntity, new();

    public Task<List<TEntity>> ListAsync<TEntity>(CancellationToken cancellationToken, Expression<Func<TEntity, bool>> expression = null, bool filterDelete = true) where TEntity : class, IDBEntity, new();


    public List<TEntity> List<TEntity, TSort>(Expression<Func<TEntity, bool>> expression = null, IDTOListor<TSort> param = null, bool filterDelete = true) where TEntity : class, IDBEntity, new() where TSort : IDTOSort, new();

    public Task<List<TEntity>> ListAsync<TEntity, TSort>(Expression<Func<TEntity, bool>> expression = null, IDTOListor<TSort> param = null, bool filterDelete = true) where TEntity : class, IDBEntity, new() where TSort : IDTOSort, new();

    public Task<List<TEntity>> ListAsync<TEntity, TSort>(CancellationToken cancellationToken, Expression<Func<TEntity, bool>> expression = null, IDTOListor<TSort> param = null, bool filterDelete = true) where TEntity : class, IDBEntity, new() where TSort : IDTOSort, new();


    public List<TEntity> ListByQueryable<TEntity, TSort>(object queryObj, IDTOListor<TSort> param = null, bool filterDelete = true) where TEntity : class, IDBEntity, new() where TSort : IDTOSort, new();

    public Task<List<TEntity>> ListByQueryableAsync<TEntity, TSort>(object queryObj, IDTOListor<TSort> param = null, bool filterDelete = true) where TEntity : class, IDBEntity, new() where TSort : IDTOSort, new();

    public Task<List<TEntity>> ListByQueryableAsync<TEntity, TSort>(CancellationToken cancellationToken, object queryObj, IDTOListor<TSort> param = null, bool filterDelete = true) where TEntity : class, IDBEntity, new() where TSort : IDTOSort, new();
}