namespace YogoPop.DB.Define;

public abstract partial class DBRepository<TEntity, TPrimaryKey> : DBRepository<TEntity>, IDBRepository<TEntity, TPrimaryKey>
    where TEntity : class, IDBEntity, IDBFPrimaryKey<TPrimaryKey>, new()
{
    //
}