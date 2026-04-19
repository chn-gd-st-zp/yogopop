namespace YogoPop.DB.Define;

public partial interface IDBRepositoryFunc<TEntity>
{
    public bool Delete(TEntity obj);

    public Task<bool> DeleteAsync(TEntity obj);

    public Task<bool> DeleteAsync(CancellationToken cancellationToken, TEntity obj);


    public bool Delete(IEnumerable<TEntity> objs);

    public Task<bool> DeleteAsync(IEnumerable<TEntity> objs);

    public Task<bool> DeleteAsync(CancellationToken cancellationToken, IEnumerable<TEntity> objs);


    public bool Delete(Expression<Func<TEntity, bool>> expression);

    public Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> expression);

    public Task<bool> DeleteAsync(CancellationToken cancellationToken, Expression<Func<TEntity, bool>> expression);
}