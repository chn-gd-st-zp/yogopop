namespace YogoPop.DB.EFCore;

public abstract partial class EFDBContext : IDBContext
{
    public virtual bool Delete<TEntity, TPrimaryKey>(TPrimaryKey pk, bool save = true) where TEntity : class, IDBEntity, IDBFPrimaryKey<TPrimaryKey>, new()
    {
        var obj = Single<TEntity, TPrimaryKey>(pk);

        return Delete(obj, save);
    }

    public virtual async Task<bool> DeleteAsync<TEntity, TPrimaryKey>(TPrimaryKey pk, bool save = true) where TEntity : class, IDBEntity, IDBFPrimaryKey<TPrimaryKey>, new()
    {
        var obj = await SingleAsync<TEntity, TPrimaryKey>(pk);

        return await DeleteAsync(obj, save);
    }

    public virtual async Task<bool> DeleteAsync<TEntity, TPrimaryKey>(CancellationToken cancellationToken, TPrimaryKey pk, bool save = true) where TEntity : class, IDBEntity, IDBFPrimaryKey<TPrimaryKey>, new()
    {
        var obj = await SingleAsync<TEntity, TPrimaryKey>(cancellationToken, pk);

        return await DeleteAsync(cancellationToken, obj, save);
    }


    public virtual bool Delete<TEntity>(TEntity obj, bool save = true) where TEntity : class, IDBEntity, new()
    {
        if (obj == null)
            return false;

        GetDBSet<TEntity>().Remove(obj);

        return save ? SaveChanges() > 0 : true;
    }

    public virtual async Task<bool> DeleteAsync<TEntity>(TEntity obj, bool save = true) where TEntity : class, IDBEntity, new()
    {
        if (obj == null)
            return false;

        GetDBSet<TEntity>().Remove(obj);

        return save ? await SaveChangesAsync() > 0 : true;
    }

    public virtual async Task<bool> DeleteAsync<TEntity>(CancellationToken cancellationToken, TEntity obj, bool save = true) where TEntity : class, IDBEntity, new()
    {
        if (obj == null)
            return false;

        GetDBSet<TEntity>().Remove(obj);

        return save ? await SaveChangesAsync(cancellationToken) > 0 : true;
    }


    public virtual bool Delete<TEntity>(IEnumerable<TEntity> objs, bool save = true) where TEntity : class, IDBEntity, new()
    {
        if (objs.IsEmpty())
            return true;

        GetDBSet<TEntity>().RemoveRange(objs);

        return save ? SaveChanges() == objs.Count() : true;
    }

    public virtual async Task<bool> DeleteAsync<TEntity>(IEnumerable<TEntity> objs, bool save = true) where TEntity : class, IDBEntity, new()
    {
        if (objs.IsEmpty())
            return true;

        GetDBSet<TEntity>().RemoveRange(objs);

        return save ? await SaveChangesAsync() == objs.Count() : true;
    }

    public virtual async Task<bool> DeleteAsync<TEntity>(CancellationToken cancellationToken, IEnumerable<TEntity> objs, bool save = true) where TEntity : class, IDBEntity, new()
    {
        if (objs.IsEmpty())
            return true;

        GetDBSet<TEntity>().RemoveRange(objs);

        return save ? await SaveChangesAsync(cancellationToken) == objs.Count() : true;
    }


    public virtual bool Delete<TEntity>(Expression<Func<TEntity, bool>> expression, bool save = true) where TEntity : class, IDBEntity, new()
    {
        var objs = List(expression);

        return Delete(objs, save);
    }

    public virtual async Task<bool> DeleteAsync<TEntity>(Expression<Func<TEntity, bool>> expression, bool save = true) where TEntity : class, IDBEntity, new()
    {
        var objs = await ListAsync(expression);

        return await DeleteAsync(objs, save);
    }

    public virtual async Task<bool> DeleteAsync<TEntity>(CancellationToken cancellationToken, Expression<Func<TEntity, bool>> expression, bool save = true) where TEntity : class, IDBEntity, new()
    {
        var objs = await ListAsync(cancellationToken, expression);

        return await DeleteAsync(cancellationToken, objs, save);
    }
}