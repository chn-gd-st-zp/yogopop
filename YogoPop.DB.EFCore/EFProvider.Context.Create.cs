namespace YogoPop.DB.EFCore;

public abstract partial class EFDBContext : IDBContext
{
    public virtual bool Create<TEntity>(TEntity obj, bool save = true) where TEntity : class, IDBEntity, new()
    {
        if (obj == null)
            return false;

        GetDBSet<TEntity>().Add(obj);

        return save ? SaveChanges() > 0 : true;
    }

    public virtual async Task<bool> CreateAsync<TEntity>(TEntity obj, bool save = true) where TEntity : class, IDBEntity, new()
    {
        if (obj == null)
            return false;

        await GetDBSet<TEntity>().AddAsync(obj);

        return save ? await SaveChangesAsync() > 0 : true;
    }

    public virtual async Task<bool> CreateAsync<TEntity>(CancellationToken cancellationToken, TEntity obj, bool save = true) where TEntity : class, IDBEntity, new()
    {
        if (obj == null)
            return false;

        await GetDBSet<TEntity>().AddAsync(obj, cancellationToken);

        return save ? await SaveChangesAsync(cancellationToken) > 0 : true;
    }


    public virtual bool Create<TEntity>(IEnumerable<TEntity> objs, bool save = true) where TEntity : class, IDBEntity, new()
    {
        if (objs.IsEmpty())
            return false;

        GetDBSet<TEntity>().AddRange(objs);

        return save ? SaveChanges() == objs.Count() : true;
    }

    public virtual async Task<bool> CreateAsync<TEntity>(IEnumerable<TEntity> objs, bool save = true) where TEntity : class, IDBEntity, new()
    {
        if (objs.IsEmpty())
            return false;

        await GetDBSet<TEntity>().AddRangeAsync(objs);

        return save ? await SaveChangesAsync() == objs.Count() : true;
    }

    public virtual async Task<bool> CreateAsync<TEntity>(CancellationToken cancellationToken, IEnumerable<TEntity> objs, bool save = true) where TEntity : class, IDBEntity, new()
    {
        if (objs.IsEmpty())
            return false;

        await GetDBSet<TEntity>().AddRangeAsync(objs, cancellationToken);

        return save ? await SaveChangesAsync(cancellationToken) == objs.Count() : true;
    }
}