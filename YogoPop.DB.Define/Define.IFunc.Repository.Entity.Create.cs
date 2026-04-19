namespace YogoPop.DB.Define;

public partial interface IDBRepositoryFunc<TEntity>
{
    public bool Create(TEntity obj);

    public Task<bool> CreateAsync(TEntity obj);

    public Task<bool> CreateAsync(CancellationToken cancellationToken, TEntity obj);


    public bool Create(IEnumerable<TEntity> objs);

    public Task<bool> CreateAsync(IEnumerable<TEntity> objs);

    public Task<bool> CreateAsync(CancellationToken cancellationToken, IEnumerable<TEntity> objs);
}