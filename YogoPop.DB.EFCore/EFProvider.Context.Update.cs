namespace YogoPop.DB.EFCore;

public abstract partial class EFDBContext : IDBContext
{
    public virtual bool Update<TEntity>(TEntity obj, bool save = true) where TEntity : class, IDBEntity, new()
    {
        if (obj == null)
            return false;

        GetDBSet<TEntity>().Update(obj);

        return save ? SaveChanges() > 0 : true;
    }

    public virtual async Task<bool> UpdateAsync<TEntity>(TEntity obj, bool save = true) where TEntity : class, IDBEntity, new()
    {
        if (obj == null)
            return false;

        GetDBSet<TEntity>().Update(obj);

        return save ? await SaveChangesAsync() > 0 : true;
    }

    public virtual async Task<bool> UpdateAsync<TEntity>(CancellationToken cancellationToken, TEntity obj, bool save = true) where TEntity : class, IDBEntity, new()
    {
        if (obj == null)
            return false;

        GetDBSet<TEntity>().Update(obj);

        return save ? await SaveChangesAsync(cancellationToken) > 0 : true;
    }


    public virtual bool Update<TEntity>(IEnumerable<TEntity> objs, bool save = true) where TEntity : class, IDBEntity, new()
    {
        if (objs.IsEmpty())
            return false;

        GetDBSet<TEntity>().UpdateRange(objs);

        return save ? SaveChanges() == objs.Count() : true;
    }

    public virtual async Task<bool> UpdateAsync<TEntity>(IEnumerable<TEntity> objs, bool save = true) where TEntity : class, IDBEntity, new()
    {
        if (objs.IsEmpty())
            return false;

        GetDBSet<TEntity>().UpdateRange(objs);

        return save ? await SaveChangesAsync() == objs.Count() : true;
    }

    public virtual async Task<bool> UpdateAsync<TEntity>(CancellationToken cancellationToken, IEnumerable<TEntity> objs, bool save = true) where TEntity : class, IDBEntity, new()
    {
        if (objs.IsEmpty())
            return false;

        GetDBSet<TEntity>().UpdateRange(objs);

        return save ? await SaveChangesAsync(cancellationToken) == objs.Count() : true;
    }


    public virtual bool Update<TEntity>(TEntity obj, IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates, bool exclude = false, bool save = true) where TEntity : class, IDBEntity, new()
    {
        if (obj == null)
            return false;

        foreach (var propertyPredicate in propertyPredicates)
        {
            this.GetPropertyEntry(obj, propertyPredicate).IsModified = !exclude;
        }

        return save ? SaveChanges() > 0 : true;
    }

    public virtual async Task<bool> UpdateAsync<TEntity>(TEntity obj, IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates, bool exclude = false, bool save = true) where TEntity : class, IDBEntity, new()
    {
        if (obj == null)
            return false;

        foreach (var propertyPredicate in propertyPredicates)
        {
            this.GetPropertyEntry(obj, propertyPredicate).IsModified = !exclude;
        }

        return save ? await SaveChangesAsync() > 0 : true;
    }

    public virtual async Task<bool> UpdateAsync<TEntity>(CancellationToken cancellationToken, TEntity obj, IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates, bool exclude = false, bool save = true) where TEntity : class, IDBEntity, new()
    {
        if (obj == null)
            return false;

        foreach (var propertyPredicate in propertyPredicates)
        {
            this.GetPropertyEntry(obj, propertyPredicate).IsModified = !exclude;
        }

        return save ? await SaveChangesAsync(cancellationToken) > 0 : true;
    }
}