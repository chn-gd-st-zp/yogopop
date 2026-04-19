namespace YogoPop.DB.Define;

public interface IDBRepository : IDBRepositoryFunc, IDisposable
{
    public IDBContext DBContext { get; }
}

public interface IDBRepository<TEntity> : IDBRepository, IDBRepositoryFunc<TEntity>
    where TEntity : class, IDBEntity, new()
{
    //
}

public interface IDBRepository<TEntity, TPrimaryKey> : IDBRepository<TEntity>, IDBRepositoryFunc<TEntity, TPrimaryKey>
    where TEntity : class, IDBEntity, IDBFPrimaryKey<TPrimaryKey>, new()
{
    //
}

public interface IDBRepository<TDBContext, TEntity, TPrimaryKey> : IDBRepository<TEntity, TPrimaryKey>
    where TDBContext : IDBContext
    where TEntity : class, IDBEntity, IDBFPrimaryKey<TPrimaryKey>, new()
{
    new TDBContext DBContext { get; }
}