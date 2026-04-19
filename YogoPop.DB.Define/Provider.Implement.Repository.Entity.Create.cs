namespace YogoPop.DB.Define;

public abstract partial class DBRepository<TEntity>
{
    public virtual bool Create(TEntity obj)
    {
        return DBContext.Create(obj);
    }

    public virtual async Task<bool> CreateAsync(TEntity obj)
    {
        return await DBContext.CreateAsync(obj);
    }

    public virtual async Task<bool> CreateAsync(CancellationToken cancellationToken, TEntity obj)
    {
        return await DBContext.CreateAsync(cancellationToken, obj);
    }


    public virtual bool Create(IEnumerable<TEntity> objs)
    {
        return DBContext.Create(objs);
    }

    public virtual async Task<bool> CreateAsync(IEnumerable<TEntity> objs)
    {
        return await DBContext.CreateAsync(objs);
    }

    public virtual async Task<bool> CreateAsync(CancellationToken cancellationToken, IEnumerable<TEntity> objs)
    {
        return await DBContext.CreateAsync(cancellationToken, objs);
    }
}