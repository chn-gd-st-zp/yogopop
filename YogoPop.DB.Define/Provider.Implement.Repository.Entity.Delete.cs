namespace YogoPop.DB.Define;

public abstract partial class DBRepository<TEntity>
{
    public virtual bool Delete(TEntity obj)
    {
        return DBContext.Delete(obj);
    }

    public virtual async Task<bool> DeleteAsync(TEntity obj)
    {
        return await DBContext.DeleteAsync(obj);
    }

    public virtual async Task<bool> DeleteAsync(CancellationToken cancellationToken, TEntity obj)
    {
        return await DBContext.DeleteAsync(cancellationToken, obj);
    }


    public virtual bool Delete(IEnumerable<TEntity> objs)
    {
        return DBContext.Delete(objs);
    }

    public virtual async Task<bool> DeleteAsync(IEnumerable<TEntity> objs)
    {
        return await DBContext.DeleteAsync(objs);
    }

    public virtual async Task<bool> DeleteAsync(CancellationToken cancellationToken, IEnumerable<TEntity> objs)
    {
        return await DBContext.DeleteAsync(cancellationToken, objs);
    }


    public virtual bool Delete(Expression<Func<TEntity, bool>> expression)
    {
        return DBContext.Delete(expression);
    }

    public virtual async Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> expression)
    {
        return await DBContext.DeleteAsync(expression);
    }

    public virtual async Task<bool> DeleteAsync(CancellationToken cancellationToken, Expression<Func<TEntity, bool>> expression)
    {
        return await DBContext.DeleteAsync(cancellationToken, expression);
    }
}