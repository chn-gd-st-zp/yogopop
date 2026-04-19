namespace YogoPop.DB.Define;

public abstract partial class DBRepository<TEntity>
{
    public virtual List<TEntity> List(Expression<Func<TEntity, bool>> expression = null, bool filterDelete = true)
    {
        return DBContext.List(expression, filterDelete);
    }

    public virtual async Task<List<TEntity>> ListAsync(Expression<Func<TEntity, bool>> expression = null, bool filterDelete = true)
    {
        return await DBContext.ListAsync(expression, filterDelete);
    }

    public virtual async Task<List<TEntity>> ListAsync(CancellationToken cancellationToken, Expression<Func<TEntity, bool>> expression = null, bool filterDelete = true)
    {
        return await DBContext.ListAsync(cancellationToken, expression, filterDelete);
    }

    public virtual List<TEntity> List<TSort>(Expression<Func<TEntity, bool>> expression = null, IDTOListor<TSort> param = null, bool filterDelete = true) where TSort : IDTOSort, new()
    {
        return DBContext.List(expression, param, filterDelete);
    }

    public virtual async Task<List<TEntity>> ListAsync<TSort>(Expression<Func<TEntity, bool>> expression = null, IDTOListor<TSort> param = null, bool filterDelete = true) where TSort : IDTOSort, new()
    {
        return await DBContext.ListAsync(expression, param, filterDelete);
    }

    public virtual async Task<List<TEntity>> ListAsync<TSort>(CancellationToken cancellationToken, Expression<Func<TEntity, bool>> expression = null, IDTOListor<TSort> param = null, bool filterDelete = true) where TSort : IDTOSort, new()
    {
        return await DBContext.ListAsync(cancellationToken, expression, param, filterDelete);
    }
}