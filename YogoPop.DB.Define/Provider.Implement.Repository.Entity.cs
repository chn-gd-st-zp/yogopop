namespace YogoPop.DB.Define;

public abstract partial class DBRepository<TEntity> : DBRepository, IDBRepository<TEntity>
    where TEntity : class, IDBEntity, new()
{
    //
}