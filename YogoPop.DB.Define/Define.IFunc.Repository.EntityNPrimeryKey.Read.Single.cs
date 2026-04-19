namespace YogoPop.DB.Define;

public partial interface IDBRepositoryFunc<TEntity, TPrimaryKey>
{
    public TEntity Single(TPrimaryKey pk, bool filterDelete = true);

    public Task<TEntity> SingleAsync(TPrimaryKey pk, bool filterDelete = true);

    public Task<TEntity> SingleAsync(CancellationToken cancellationToken, TPrimaryKey pk, bool filterDelete = true);
}