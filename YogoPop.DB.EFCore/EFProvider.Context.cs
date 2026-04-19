namespace YogoPop.DB.EFCore;

public abstract partial class EFDBContext : DbContext, IDBContext
{
    private string _id = Unique.GetGUID();
    public string ID { get { return _id; } }

    public EFDBContext(EFDBContextOptionsBuilder optionsBuilder) : base(optionsBuilder.BulidAction(optionsBuilder))
    {
        DBSets = new Dictionary<Type, object>();
        InitDBSets();
    }

    public IQueryable<TEntity> GetQueryable<TEntity>(bool filterDelete = true) where TEntity : class, IDBEntity, new()
    {
        var query = GetDBSet<TEntity>().AsQueryable<TEntity>();

        if (filterDelete)
            query = query.FilteDelete();

        return query;
    }

    public async Task<int> SaveChangesAsync() => await base.SaveChangesAsync();

    public object GetQueryObject<TEntity>(bool filterDelete = true) where TEntity : class, IDBEntity, new() => GetQueryable<TEntity>(filterDelete);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        BindMaps(modelBuilder);
    }

    protected abstract void BindMaps(ModelBuilder modelBuilder);

    private readonly Dictionary<Type, object> DBSets;

    protected void AddDBSet<TEntity>(DbSet<TEntity> obj) where TEntity : class, IDBEntity, new() { DBSets.Add(typeof(TEntity), obj); }

    protected DbSet<TEntity> GetDBSet<TEntity>() where TEntity : class, IDBEntity, new() { return DBSets[typeof(TEntity)] as DbSet<TEntity>; }

    protected abstract void InitDBSets();
}