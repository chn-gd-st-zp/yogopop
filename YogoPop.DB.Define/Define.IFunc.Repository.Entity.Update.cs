namespace YogoPop.DB.Define;

public partial interface IDBRepositoryFunc<TEntity>
{
    public bool Update(TEntity obj);

    public Task<bool> UpdateAsync(TEntity obj);

    public Task<bool> UpdateAsync(CancellationToken cancellationToken, TEntity obj);


    public bool Update(IEnumerable<TEntity> objs);

    public Task<bool> UpdateAsync(IEnumerable<TEntity> objs);

    public Task<bool> UpdateAsync(CancellationToken cancellationToken, IEnumerable<TEntity> objs);


    public bool Update(TEntity obj, IEnumerable<Expression<Func<TEntity, object>>> propertyPredicatesexclude, bool exclude = false);

    public Task<bool> UpdateAsync(TEntity obj, IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates, bool exclude = false);

    public Task<bool> UpdateAsync(CancellationToken cancellationToken, TEntity obj, IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates, bool exclude = false);
}