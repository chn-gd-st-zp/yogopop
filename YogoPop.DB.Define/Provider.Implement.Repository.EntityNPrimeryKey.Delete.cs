namespace YogoPop.DB.Define;

public abstract partial class DBRepository<TEntity, TPrimaryKey>
{
    public virtual bool Delete(TPrimaryKey pk)
    {
        return DBContext.Delete<TEntity, TPrimaryKey>(pk);
    }

    public virtual async Task<bool> DeleteAsync(TPrimaryKey pk)
    {
        return await DBContext.DeleteAsync<TEntity, TPrimaryKey>(pk);
    }

    public virtual async Task<bool> DeleteAsync(CancellationToken cancellationToken, TPrimaryKey pk)
    {
        return await DBContext.DeleteAsync<TEntity, TPrimaryKey>(cancellationToken, pk);
    }
}