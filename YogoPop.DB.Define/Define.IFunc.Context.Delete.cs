namespace YogoPop.DB.Define;

public partial interface IDBContextFunc
{
    public bool Delete<TEntity, TPrimaryKey>(TPrimaryKey pk, bool save = true) where TEntity : class, IDBEntity, IDBFPrimaryKey<TPrimaryKey>, new();

    public Task<bool> DeleteAsync<TEntity, TPrimaryKey>(TPrimaryKey pk, bool save = true) where TEntity : class, IDBEntity, IDBFPrimaryKey<TPrimaryKey>, new();

    public Task<bool> DeleteAsync<TEntity, TPrimaryKey>(CancellationToken cancellationToken, TPrimaryKey pk, bool save = true) where TEntity : class, IDBEntity, IDBFPrimaryKey<TPrimaryKey>, new();


    public bool Delete<TEntity>(TEntity obj, bool save = true) where TEntity : class, IDBEntity, new();

    public Task<bool> DeleteAsync<TEntity>(TEntity obj, bool save = true) where TEntity : class, IDBEntity, new();

    public Task<bool> DeleteAsync<TEntity>(CancellationToken cancellationToken, TEntity obj, bool save = true) where TEntity : class, IDBEntity, new();


    public bool Delete<TEntity>(IEnumerable<TEntity> objs, bool save = true) where TEntity : class, IDBEntity, new();

    public Task<bool> DeleteAsync<TEntity>(IEnumerable<TEntity> objs, bool save = true) where TEntity : class, IDBEntity, new();

    public Task<bool> DeleteAsync<TEntity>(CancellationToken cancellationToken, IEnumerable<TEntity> objs, bool save = true) where TEntity : class, IDBEntity, new();


    public bool Delete<TEntity>(Expression<Func<TEntity, bool>> expression, bool save = true) where TEntity : class, IDBEntity, new();

    public Task<bool> DeleteAsync<TEntity>(Expression<Func<TEntity, bool>> expression, bool save = true) where TEntity : class, IDBEntity, new();

    public Task<bool> DeleteAsync<TEntity>(CancellationToken cancellationToken, Expression<Func<TEntity, bool>> expression, bool save = true) where TEntity : class, IDBEntity, new();
}