namespace YogoPop.DB.Define;

public partial interface IDBContextFunc
{
    public bool Update<TEntity>(TEntity obj, bool save = true) where TEntity : class, IDBEntity, new();

    public Task<bool> UpdateAsync<TEntity>(TEntity obj, bool save = true) where TEntity : class, IDBEntity, new();

    public Task<bool> UpdateAsync<TEntity>(CancellationToken cancellationToken, TEntity obj, bool save = true) where TEntity : class, IDBEntity, new();


    public bool Update<TEntity>(IEnumerable<TEntity> objs, bool save = true) where TEntity : class, IDBEntity, new();

    public Task<bool> UpdateAsync<TEntity>(IEnumerable<TEntity> objs, bool save = true) where TEntity : class, IDBEntity, new();

    public Task<bool> UpdateAsync<TEntity>(CancellationToken cancellationToken, IEnumerable<TEntity> objs, bool save = true) where TEntity : class, IDBEntity, new();


    public bool Update<TEntity>(TEntity obj, IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates, bool exclude = false, bool save = true) where TEntity : class, IDBEntity, new();

    public Task<bool> UpdateAsync<TEntity>(TEntity obj, IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates, bool exclude = false, bool save = true) where TEntity : class, IDBEntity, new();

    public Task<bool> UpdateAsync<TEntity>(CancellationToken cancellationToken, TEntity obj, IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates, bool exclude = false, bool save = true) where TEntity : class, IDBEntity, new();
}