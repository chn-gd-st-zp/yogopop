namespace YogoPop.DB.Define;

public partial interface IDBRepositoryFunc<TEntity, TPrimaryKey>
{
    public bool Delete(TPrimaryKey pk);

    public Task<bool> DeleteAsync(TPrimaryKey pk);

    public Task<bool> DeleteAsync(CancellationToken cancellationToken, TPrimaryKey pk);
}