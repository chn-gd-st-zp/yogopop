namespace YogoPop.DB.EFCore;

public abstract class EFDBRepository : DBRepository
{
    //
}

public abstract class EFDBRepository<TEntity> : DBRepository<TEntity>
    where TEntity : class, IDBEntity, new()
{
    //
}

public abstract class EFDBRepository<TEntity, TPrimaryKey> : DBRepository<TEntity, TPrimaryKey>
    where TEntity : class, IDBEntity, IDBFPrimaryKey<TPrimaryKey>, new()
{
    //
}

public abstract class EFDBRepository<TDBContext, TEntity, TKey> : DBRepository<TDBContext, TEntity, TKey>
    where TDBContext : IDBContext
    where TEntity : class, IDBEntity, IDBFPrimaryKey<TKey>, new()
{
    //
}