namespace YogoPop.DB.Define;

public interface IDBContext : IDBContextFunc, IDisposable
{
    public string ID { get; }

    public int SaveChanges();

    public Task<int> SaveChangesAsync();

    public object GetQueryObject<TEntity>(bool filterDelete = true) where TEntity : class, IDBEntity, new();
}

public interface IDBContextOptionsBuilder
{
    //
}