namespace YogoPop.DB.EFCore;

public abstract partial class EFDBContext : IDBContext
{
    public virtual Tuple<List<TEntity>, int> Page<TEntity, TSort>(Expression<Func<TEntity, bool>> expression = null, IDTOPager<TSort> param = null, bool filterDelete = true) where TEntity : class, IDBEntity, new() where TSort : IDTOSort, new()
    {
        var query = GetQueryable<TEntity>(filterDelete);

        if (expression != null)
            query = query.Where(expression);

        return PageByQueryable<TEntity, TSort>(query, param);
    }

    public virtual async Task<Tuple<List<TEntity>, int>> PageAsync<TEntity, TSort>(Expression<Func<TEntity, bool>> expression = null, IDTOPager<TSort> param = null, bool filterDelete = true) where TEntity : class, IDBEntity, new() where TSort : IDTOSort, new()
    {
        var query = GetQueryable<TEntity>(filterDelete);

        if (expression != null)
            query = query.Where(expression);

        return await PageByQueryableAsync<TEntity, TSort>(query, param);
    }

    public virtual async Task<Tuple<List<TEntity>, int>> PageAsync<TEntity, TSort>(CancellationToken cancellationToken, Expression<Func<TEntity, bool>> expression = null, IDTOPager<TSort> param = null, bool filterDelete = true) where TEntity : class, IDBEntity, new() where TSort : IDTOSort, new()
    {
        var query = GetQueryable<TEntity>(filterDelete);

        if (expression != null)
            query = query.Where(expression);

        return await PageByQueryableAsync<TEntity, TSort>(cancellationToken, query, param);
    }


    public virtual Tuple<List<TEntity>, int> PageByQueryable<TEntity, TSort>(object queryObj, IDTOPager<TSort> param = null, bool filterDelete = true) where TEntity : class, IDBEntity, new() where TSort : IDTOSort, new()
    {
        var query = queryObj == null ? GetQueryable<TEntity>(filterDelete) : (IQueryable<TEntity>)queryObj;

        if (param == null)
            param = InjectionContext.Resolve<IDTOPager<TSort>>();

        int rowQty = query.Count();

        query = OrderBy<TEntity, TSort>(query, param) as IQueryable<TEntity>;

        param.PageSize = param == null || param.PageSize == 0 ? 10 : param.PageSize;
        param.PageIndex = param == null || param.PageIndex == 0 ? 1 : param.PageIndex;

        var dataList = query.Skip(param.PageSize * (param.PageIndex - 1)).Take(param.PageSize).ToList();

        return new Tuple<List<TEntity>, int>(dataList, rowQty);
    }

    public virtual async Task<Tuple<List<TEntity>, int>> PageByQueryableAsync<TEntity, TSort>(object queryObj, IDTOPager<TSort> param = null, bool filterDelete = true) where TEntity : class, IDBEntity, new() where TSort : IDTOSort, new()
    {
        var query = queryObj == null ? GetQueryable<TEntity>(filterDelete) : (IQueryable<TEntity>)queryObj;

        if (param == null)
            param = InjectionContext.Resolve<IDTOPager<TSort>>();

        int rowQty = query.Count();

        query = OrderBy<TEntity, TSort>(query, param) as IQueryable<TEntity>;

        param.PageSize = param.PageSize == 0 ? 10 : param.PageSize;
        param.PageIndex = param.PageIndex == 0 ? 1 : param.PageIndex;

        var dataList = await query.Skip(param.PageSize * (param.PageIndex - 1)).Take(param.PageSize).ToListAsync();

        return new Tuple<List<TEntity>, int>(dataList, rowQty);
    }

    public virtual async Task<Tuple<List<TEntity>, int>> PageByQueryableAsync<TEntity, TSort>(CancellationToken cancellationToken, object queryObj, IDTOPager<TSort> param = null, bool filterDelete = true) where TEntity : class, IDBEntity, new() where TSort : IDTOSort, new()
    {
        var query = queryObj == null ? GetQueryable<TEntity>(filterDelete) : (IQueryable<TEntity>)queryObj;

        if (param == null)
            param = InjectionContext.Resolve<IDTOPager<TSort>>();

        int rowQty = query.Count();

        query = OrderBy<TEntity, TSort>(query, param) as IQueryable<TEntity>;

        param.PageSize = param.PageSize == 0 ? 10 : param.PageSize;
        param.PageIndex = param.PageIndex == 0 ? 1 : param.PageIndex;

        var dataList = await query.Skip(param.PageSize * (param.PageIndex - 1)).Take(param.PageSize).ToListAsync(cancellationToken);

        return new Tuple<List<TEntity>, int>(dataList, rowQty);
    }
}