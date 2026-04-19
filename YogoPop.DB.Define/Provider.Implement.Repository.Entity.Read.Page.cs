namespace YogoPop.DB.Define;

public abstract partial class DBRepository<TEntity>
{
    public virtual Tuple<List<TEntity>, int> Page<TSort>(Expression<Func<TEntity, bool>> expression, IDTOPager<TSort> param = null, bool filterDelete = true) where TSort : IDTOSort, new()
    {
        return DBContext.Page(expression, param, filterDelete);
    }

    public virtual async Task<Tuple<List<TEntity>, int>> PageAsync<TSort>(Expression<Func<TEntity, bool>> expression, IDTOPager<TSort> param = null, bool filterDelete = true) where TSort : IDTOSort, new()
    {
        return await DBContext.PageAsync(expression, param, filterDelete);
    }

    public virtual async Task<Tuple<List<TEntity>, int>> PageAsync<TSort>(CancellationToken cancellationToken, Expression<Func<TEntity, bool>> expression, IDTOPager<TSort> param = null, bool filterDelete = true) where TSort : IDTOSort, new()
    {
        return await DBContext.PageAsync(cancellationToken, expression, param, filterDelete);
    }
}