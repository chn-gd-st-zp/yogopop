namespace YogoPop.DB.Define;

public abstract partial class DBRepository
{
    public virtual List<TEntity> ListByQueryable<TEntity, TSort>(object queryObj, IDTOListor<TSort> param = null, bool filterDelete = true) where TEntity : class, IDBEntity, new() where TSort : IDTOSort, new()
    {
        return DBContext.ListByQueryable<TEntity, TSort>(queryObj, param, filterDelete);
    }

    public virtual async Task<List<TEntity>> ListByQueryableAsync<TEntity, TSort>(object queryObj, IDTOListor<TSort> param = null, bool filterDelete = true) where TEntity : class, IDBEntity, new() where TSort : IDTOSort, new()
    {
        return await DBContext.ListByQueryableAsync<TEntity, TSort>(queryObj, param, filterDelete);
    }

    public virtual async Task<List<TEntity>> ListByQueryableAsync<TEntity, TSort>(CancellationToken cancellationToken, object queryObj, IDTOListor<TSort> param = null, bool filterDelete = true) where TEntity : class, IDBEntity, new() where TSort : IDTOSort, new()
    {
        return await DBContext.ListByQueryableAsync<TEntity, TSort>(cancellationToken, queryObj, param, filterDelete);
    }
}