namespace YogoPop.DB.Define;

public partial interface IDBRepositoryFunc<TEntity>
{
    public List<TEntity> List(Expression<Func<TEntity, bool>> expression = null, bool filterDelete = true);

    public Task<List<TEntity>> ListAsync(Expression<Func<TEntity, bool>> expression = null, bool filterDelete = true);

    public Task<List<TEntity>> ListAsync(CancellationToken cancellationToken, Expression<Func<TEntity, bool>> expression = null, bool filterDelete = true);


    public List<TEntity> List<TSort>(Expression<Func<TEntity, bool>> expression = null, IDTOListor<TSort> param = null, bool filterDelete = true) where TSort : IDTOSort, new();

    public Task<List<TEntity>> ListAsync<TSort>(Expression<Func<TEntity, bool>> expression = null, IDTOListor<TSort> param = null, bool filterDelete = true) where TSort : IDTOSort, new();

    public Task<List<TEntity>> ListAsync<TSort>(CancellationToken cancellationToken, Expression<Func<TEntity, bool>> expression = null, IDTOListor<TSort> param = null, bool filterDelete = true) where TSort : IDTOSort, new();
}