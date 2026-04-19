namespace YogoPop.DB.Define;

public abstract partial class DBRepository : IDBRepository
{
    public DBRepository() { DBContext = GetDBContext(); }

    ~DBRepository() => Dispose();

    public void Dispose()
    {
        DBContext.Dispose();
        ServiceScope.Dispose();
        GC.SuppressFinalize(this);
    }

    protected IServiceScope ServiceScope = InjectionContext.Root.CreateScope();

    public virtual IDBContext DBContext { get; protected set; }

    protected virtual IDBContext GetDBContext() { return ServiceScope.Resolve<IDBContext>(); }
}