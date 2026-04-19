namespace YogoPop.DB.EFCore;

public abstract partial class EFDBContext : IDBContext
{
    public virtual TEntity Single<TEntity, TPrimaryKey>(TPrimaryKey pk, bool filterDelete = true) where TEntity : class, IDBEntity, IDBFPrimaryKey<TPrimaryKey>, new()
    {
        return GetQueryable<TEntity>(filterDelete).Where(o => o.PrimaryKey.Equals(pk)).SingleOrDefault();
    }

    public virtual async Task<TEntity> SingleAsync<TEntity, TPrimaryKey>(TPrimaryKey pk, bool filterDelete = true) where TEntity : class, IDBEntity, IDBFPrimaryKey<TPrimaryKey>, new()
    {
        return await GetQueryable<TEntity>(filterDelete).Where(o => o.PrimaryKey.Equals(pk)).SingleOrDefaultAsync();
    }

    public virtual async Task<TEntity> SingleAsync<TEntity, TPrimaryKey>(CancellationToken cancellationToken, TPrimaryKey pk, bool filterDelete = true) where TEntity : class, IDBEntity, IDBFPrimaryKey<TPrimaryKey>, new()
    {
        return await GetQueryable<TEntity>(filterDelete).Where(o => o.PrimaryKey.Equals(pk)).SingleOrDefaultAsync(cancellationToken);
    }


    public virtual TEntity Single<TEntity>(Expression<Func<TEntity, bool>> expression, bool filterDelete = true) where TEntity : class, IDBEntity, new()
    {
        return GetQueryable<TEntity>(filterDelete).Where(expression).SingleOrDefault();
    }

    public virtual async Task<TEntity> SingleAsync<TEntity>(Expression<Func<TEntity, bool>> expression, bool filterDelete = true) where TEntity : class, IDBEntity, new()
    {
        return await GetQueryable<TEntity>(filterDelete).Where(expression).SingleOrDefaultAsync();
    }

    public virtual async Task<TEntity> SingleAsync<TEntity>(CancellationToken cancellationToken, Expression<Func<TEntity, bool>> expression, bool filterDelete = true) where TEntity : class, IDBEntity, new()
    {
        return await GetQueryable<TEntity>(filterDelete).Where(expression).SingleOrDefaultAsync(cancellationToken);
    }


    public virtual TEntity FirstByASC<TEntity, TOrderByField>(Expression<Func<TEntity, bool>> whereBy, Expression<Func<TEntity, TOrderByField>> orderBy, bool filterDelete = true) where TEntity : class, IDBEntity, new()
    {
        return GetQueryable<TEntity>(filterDelete).Where(whereBy).OrderBy(orderBy).FirstOrDefault();
    }

    public virtual async Task<TEntity> FirstByASCAsync<TEntity, TOrderByField>(Expression<Func<TEntity, bool>> whereBy, Expression<Func<TEntity, TOrderByField>> orderBy, bool filterDelete = true) where TEntity : class, IDBEntity, new()
    {
        return await GetQueryable<TEntity>(filterDelete).Where(whereBy).OrderBy(orderBy).FirstOrDefaultAsync();
    }

    public virtual async Task<TEntity> FirstByASCAsync<TEntity, TOrderByField>(CancellationToken cancellationToken, Expression<Func<TEntity, bool>> whereBy, Expression<Func<TEntity, TOrderByField>> orderBy, bool filterDelete = true) where TEntity : class, IDBEntity, new()
    {
        return await GetQueryable<TEntity>(filterDelete).Where(whereBy).OrderBy(orderBy).FirstOrDefaultAsync(cancellationToken);
    }


    public virtual TEntity FirstByDESC<TEntity, TOrderByField>(Expression<Func<TEntity, bool>> whereBy, Expression<Func<TEntity, TOrderByField>> orderBy, bool filterDelete = true) where TEntity : class, IDBEntity, new()
    {
        return GetQueryable<TEntity>(filterDelete).Where(whereBy).OrderByDescending(orderBy).FirstOrDefault();
    }

    public virtual async Task<TEntity> FirstByDESCAsync<TEntity, TOrderByField>(Expression<Func<TEntity, bool>> whereBy, Expression<Func<TEntity, TOrderByField>> orderBy, bool filterDelete = true) where TEntity : class, IDBEntity, new()
    {
        return await GetQueryable<TEntity>(filterDelete).Where(whereBy).OrderByDescending(orderBy).FirstOrDefaultAsync();
    }

    public virtual async Task<TEntity> FirstByDESCAsync<TEntity, TOrderByField>(CancellationToken cancellationToken, Expression<Func<TEntity, bool>> whereBy, Expression<Func<TEntity, TOrderByField>> orderBy, bool filterDelete = true) where TEntity : class, IDBEntity, new()
    {
        return await GetQueryable<TEntity>(filterDelete).Where(whereBy).OrderByDescending(orderBy).FirstOrDefaultAsync(cancellationToken);
    }
}