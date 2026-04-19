namespace YogoPop.DB.Define;

public abstract class DBRepository<TDBContext, TEntity, TKey> : DBRepository<TEntity, TKey>, IDBRepository<TDBContext, TEntity, TKey>
       where TDBContext : IDBContext
       where TEntity : class, IDBEntity, IDBFPrimaryKey<TKey>, new()
{
    public DBRepository() { DBContext = (TDBContext)GetDBContext(); }

    private TDBContext _dbContext;
    public new TDBContext DBContext { get { return _dbContext; } protected set { _dbContext = value; base.DBContext = value; } }

    protected override IDBContext GetDBContext() { return ServiceScope.Resolve<TDBContext>(); }
}