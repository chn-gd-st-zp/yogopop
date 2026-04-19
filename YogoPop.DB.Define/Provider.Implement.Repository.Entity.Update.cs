namespace YogoPop.DB.Define;

public abstract partial class DBRepository<TEntity>
{
    public virtual bool Update(TEntity obj)
    {
        return DBContext.Update(obj);
    }

    public virtual async Task<bool> UpdateAsync(TEntity obj)
    {
        return await DBContext.UpdateAsync(obj);
    }

    public virtual async Task<bool> UpdateAsync(CancellationToken cancellationToken, TEntity obj)
    {
        return await DBContext.UpdateAsync(cancellationToken, obj);
    }


    public virtual bool Update(IEnumerable<TEntity> objs)
    {
        return DBContext.Update(objs);
    }

    public virtual async Task<bool> UpdateAsync(IEnumerable<TEntity> objs)
    {
        return await DBContext.UpdateAsync(objs);
    }

    public virtual async Task<bool> UpdateAsync(CancellationToken cancellationToken, IEnumerable<TEntity> objs)
    {
        return await DBContext.UpdateAsync(cancellationToken, objs);
    }


    public virtual bool Update(TEntity obj, IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates, bool exclude = false)
    {
        return DBContext.Update(obj, propertyPredicates, exclude);
    }

    public virtual async Task<bool> UpdateAsync(TEntity obj, IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates, bool exclude = false)
    {
        return await DBContext.UpdateAsync(obj, propertyPredicates, exclude);
    }

    public virtual async Task<bool> UpdateAsync(CancellationToken cancellationToken, TEntity obj, IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates, bool exclude = false)
    {
        return await DBContext.UpdateAsync(cancellationToken, obj, propertyPredicates, exclude);
    }
}