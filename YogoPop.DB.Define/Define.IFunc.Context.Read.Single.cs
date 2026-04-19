namespace YogoPop.DB.Define;

public partial interface IDBContextFunc
{
    public TEntity Single<TEntity, TPrimaryKey>(TPrimaryKey pk, bool filterDelete = true) where TEntity : class, IDBEntity, IDBFPrimaryKey<TPrimaryKey>, new();

    public Task<TEntity> SingleAsync<TEntity, TPrimaryKey>(TPrimaryKey pk, bool filterDelete = true) where TEntity : class, IDBEntity, IDBFPrimaryKey<TPrimaryKey>, new();

    public Task<TEntity> SingleAsync<TEntity, TPrimaryKey>(CancellationToken cancellationToken, TPrimaryKey pk, bool filterDelete = true) where TEntity : class, IDBEntity, IDBFPrimaryKey<TPrimaryKey>, new();


    public TEntity Single<TEntity>(Expression<Func<TEntity, bool>> expression, bool filterDelete = true) where TEntity : class, IDBEntity, new();

    public Task<TEntity> SingleAsync<TEntity>(Expression<Func<TEntity, bool>> expression, bool filterDelete = true) where TEntity : class, IDBEntity, new();

    public Task<TEntity> SingleAsync<TEntity>(CancellationToken cancellationToken, Expression<Func<TEntity, bool>> expression, bool filterDelete = true) where TEntity : class, IDBEntity, new();


    public TEntity FirstByASC<TEntity, TOrderByField>(Expression<Func<TEntity, bool>> whereBy, Expression<Func<TEntity, TOrderByField>> orderBy, bool filterDelete = true) where TEntity : class, IDBEntity, new();

    public Task<TEntity> FirstByASCAsync<TEntity, TOrderByField>(Expression<Func<TEntity, bool>> whereBy, Expression<Func<TEntity, TOrderByField>> orderBy, bool filterDelete = true) where TEntity : class, IDBEntity, new();

    public Task<TEntity> FirstByASCAsync<TEntity, TOrderByField>(CancellationToken cancellationToken, Expression<Func<TEntity, bool>> whereBy, Expression<Func<TEntity, TOrderByField>> orderBy, bool filterDelete = true) where TEntity : class, IDBEntity, new();


    public TEntity FirstByDESC<TEntity, TOrderByField>(Expression<Func<TEntity, bool>> whereBy, Expression<Func<TEntity, TOrderByField>> orderBy, bool filterDelete = true) where TEntity : class, IDBEntity, new();

    public Task<TEntity> FirstByDESCAsync<TEntity, TOrderByField>(Expression<Func<TEntity, bool>> whereBy, Expression<Func<TEntity, TOrderByField>> orderBy, bool filterDelete = true) where TEntity : class, IDBEntity, new();

    public Task<TEntity> FirstByDESCAsync<TEntity, TOrderByField>(CancellationToken cancellationToken, Expression<Func<TEntity, bool>> whereBy, Expression<Func<TEntity, TOrderByField>> orderBy, bool filterDelete = true) where TEntity : class, IDBEntity, new();
}