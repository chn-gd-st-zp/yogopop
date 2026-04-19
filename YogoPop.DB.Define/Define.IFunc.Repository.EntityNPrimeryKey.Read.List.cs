namespace YogoPop.DB.Define;

public partial interface IDBRepositoryFunc<TEntity, TPrimaryKey>
{
    public List<TEntity> List<TSort>(TPrimaryKey[] pks, IDTOListor<TSort> param = null, bool filterDelete = true) where TSort : IDTOSort, new();

    public Task<List<TEntity>> ListAsync<TSort>(TPrimaryKey[] pks, IDTOListor<TSort> param = null, bool filterDelete = true) where TSort : IDTOSort, new();

    public Task<List<TEntity>> ListAsync<TSort>(CancellationToken cancellationToken, TPrimaryKey[] pks, IDTOListor<TSort> param = null, bool filterDelete = true) where TSort : IDTOSort, new();
}