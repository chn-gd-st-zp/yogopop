namespace YogoPop.DB.Define;

public partial interface IDBContextFunc
{
    public bool Create<TEntity>(TEntity obj, bool save = true) where TEntity : class, IDBEntity, new();

    public Task<bool> CreateAsync<TEntity>(TEntity obj, bool save = true) where TEntity : class, IDBEntity, new();

    public Task<bool> CreateAsync<TEntity>(CancellationToken cancellationToken, TEntity obj, bool save = true) where TEntity : class, IDBEntity, new();


    public bool Create<TEntity>(IEnumerable<TEntity> objs, bool save = true) where TEntity : class, IDBEntity, new();

    public Task<bool> CreateAsync<TEntity>(IEnumerable<TEntity> objs, bool save = true) where TEntity : class, IDBEntity, new();

    public Task<bool> CreateAsync<TEntity>(CancellationToken cancellationToken, IEnumerable<TEntity> objs, bool save = true) where TEntity : class, IDBEntity, new();
}