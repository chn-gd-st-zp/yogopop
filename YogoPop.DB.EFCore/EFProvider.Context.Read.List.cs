namespace YogoPop.DB.EFCore;

public abstract partial class EFDBContext : IDBContext
{
    public virtual List<TEntity> List<TEntity, TPrimaryKey, TSort>(TPrimaryKey[] pks, IDTOListor<TSort> param = null, bool filterDelete = true) where TEntity : class, IDBEntity, IDBFPrimaryKey<TPrimaryKey>, new() where TSort : IDTOSort, new()
    {
        var query = GetQueryable<TEntity>(filterDelete);

        if (pks != null && pks.Length != 0)
            query = query.Where(o => pks.Contains(o.PrimaryKey));

        query = OrderBy<TEntity, TSort>(query, param) as IQueryable<TEntity>;

        return query.ToList();
    }

    public virtual async Task<List<TEntity>> ListAsync<TEntity, TPrimaryKey, TSort>(TPrimaryKey[] pks, IDTOListor<TSort> param = null, bool filterDelete = true) where TEntity : class, IDBEntity, IDBFPrimaryKey<TPrimaryKey>, new() where TSort : IDTOSort, new()
    {
        var query = GetQueryable<TEntity>(filterDelete);

        if (pks != null && pks.Length != 0)
            query = query.Where(o => pks.Contains(o.PrimaryKey));

        query = OrderBy<TEntity, TSort>(query, param) as IQueryable<TEntity>;

        return await query.ToListAsync();
    }

    public virtual async Task<List<TEntity>> ListAsync<TEntity, TPrimaryKey, TSort>(CancellationToken cancellationToken, TPrimaryKey[] pks, IDTOListor<TSort> param = null, bool filterDelete = true) where TEntity : class, IDBEntity, IDBFPrimaryKey<TPrimaryKey>, new() where TSort : IDTOSort, new()
    {
        var query = GetQueryable<TEntity>(filterDelete);

        if (pks != null && pks.Length != 0)
            query = query.Where(o => pks.Contains(o.PrimaryKey));

        query = OrderBy<TEntity, TSort>(query, param) as IQueryable<TEntity>;

        return await query.ToListAsync(cancellationToken);
    }


    public virtual List<TEntity> List<TEntity>(Expression<Func<TEntity, bool>> expression = null, bool filterDelete = true) where TEntity : class, IDBEntity, new()
    {
        var query = GetQueryable<TEntity>(filterDelete);

        if (expression != null)
            query = query.Where(expression);

        return query.ToList();
    }

    public virtual async Task<List<TEntity>> ListAsync<TEntity>(Expression<Func<TEntity, bool>> expression = null, bool filterDelete = true) where TEntity : class, IDBEntity, new()
    {
        var query = GetQueryable<TEntity>(filterDelete);

        if (expression != null)
            query = query.Where(expression);

        return query.ToList();
    }

    public virtual async Task<List<TEntity>> ListAsync<TEntity>(CancellationToken cancellationToken, Expression<Func<TEntity, bool>> expression = null, bool filterDelete = true) where TEntity : class, IDBEntity, new()
    {
        var query = GetQueryable<TEntity>(filterDelete);

        if (expression != null)
            query = query.Where(expression);

        return await query.ToListAsync(cancellationToken);
    }


    public virtual List<TEntity> List<TEntity, TSort>(Expression<Func<TEntity, bool>> expression = null, IDTOListor<TSort> param = null, bool filterDelete = true) where TEntity : class, IDBEntity, new() where TSort : IDTOSort, new()
    {
        var query = GetQueryable<TEntity>(filterDelete);

        if (expression != null)
            query = query.Where(expression);

        query = OrderBy<TEntity, TSort>(query, param) as IQueryable<TEntity>;

        return query.ToList();
    }

    public virtual async Task<List<TEntity>> ListAsync<TEntity, TSort>(Expression<Func<TEntity, bool>> expression = null, IDTOListor<TSort> param = null, bool filterDelete = true) where TEntity : class, IDBEntity, new() where TSort : IDTOSort, new()
    {
        var query = GetQueryable<TEntity>(filterDelete);

        if (expression != null)
            query = query.Where(expression);

        query = OrderBy<TEntity, TSort>(query, param) as IQueryable<TEntity>;

        return query.ToList();
    }

    public virtual async Task<List<TEntity>> ListAsync<TEntity, TSort>(CancellationToken cancellationToken, Expression<Func<TEntity, bool>> expression = null, IDTOListor<TSort> param = null, bool filterDelete = true) where TEntity : class, IDBEntity, new() where TSort : IDTOSort, new()
    {
        var query = GetQueryable<TEntity>(filterDelete);

        if (expression != null)
            query = query.Where(expression);

        query = OrderBy<TEntity, TSort>(query, param) as IQueryable<TEntity>;

        return await query.ToListAsync(cancellationToken);
    }


    public virtual List<TEntity> ListByQueryable<TEntity, TSort>(object queryObj, IDTOListor<TSort> param = null, bool filterDelete = true) where TEntity : class, IDBEntity, new() where TSort : IDTOSort, new()
    {
        var query = queryObj == null ? GetQueryable<TEntity>(filterDelete) : (IQueryable<TEntity>)queryObj;

        if (param == null)
            param = InjectionContext.Resolve<IDTOListor<TSort>>();

        query = OrderBy<TEntity, TSort>(query, param) as IQueryable<TEntity>;

        return query.ToList();
    }

    public virtual async Task<List<TEntity>> ListByQueryableAsync<TEntity, TSort>(object queryObj, IDTOListor<TSort> param = null, bool filterDelete = true) where TEntity : class, IDBEntity, new() where TSort : IDTOSort, new()
    {
        var query = queryObj == null ? GetQueryable<TEntity>(filterDelete) : (IQueryable<TEntity>)queryObj;

        if (param == null)
            param = InjectionContext.Resolve<IDTOListor<TSort>>();

        query = OrderBy<TEntity, TSort>(query, param) as IQueryable<TEntity>;

        return await query.ToListAsync();
    }

    public virtual async Task<List<TEntity>> ListByQueryableAsync<TEntity, TSort>(CancellationToken cancellationToken, object queryObj, IDTOListor<TSort> param = null, bool filterDelete = true) where TEntity : class, IDBEntity, new() where TSort : IDTOSort, new()
    {
        var query = queryObj == null ? GetQueryable<TEntity>(filterDelete) : (IQueryable<TEntity>)queryObj;

        if (param == null)
            param = InjectionContext.Resolve<IDTOListor<TSort>>();

        query = OrderBy<TEntity, TSort>(query, param) as IQueryable<TEntity>;

        return await query.ToListAsync(cancellationToken);
    }
}