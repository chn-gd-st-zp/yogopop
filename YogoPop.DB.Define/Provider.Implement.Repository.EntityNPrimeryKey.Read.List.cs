namespace YogoPop.DB.Define;

public abstract partial class DBRepository<TEntity, TPrimaryKey>
{
    public virtual List<TEntity> List<TSort>(TPrimaryKey[] pks, IDTOListor<TSort> param = null, bool filterDelete = true) where TSort : IDTOSort, new()
    {
        return DBContext.List<TEntity, TPrimaryKey, TSort>(pks, param, filterDelete);
    }

    public virtual async Task<List<TEntity>> ListAsync<TSort>(TPrimaryKey[] pks, IDTOListor<TSort> param = null, bool filterDelete = true) where TSort : IDTOSort, new()
    {
        return await DBContext.ListAsync<TEntity, TPrimaryKey, TSort>(pks, param, filterDelete);
    }

    public virtual async Task<List<TEntity>> ListAsync<TSort>(CancellationToken cancellationToken, TPrimaryKey[] pks, IDTOListor<TSort> param = null, bool filterDelete = true) where TSort : IDTOSort, new()
    {
        return await DBContext.ListAsync<TEntity, TPrimaryKey, TSort>(cancellationToken, pks, param, filterDelete);
    }
}