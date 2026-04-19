namespace YogoPop.DB.Define;

public partial interface IDBContextFunc
{
    public object OrderBy<TEntity, TSort>(object queryable, IDTOSearch<TSort> sorts) where TSort : IDTOSort, new();

    public string GetNextSequence<TEntity>() where TEntity : class, IDBEntity, IDBFSequence, new();

    public Task<string> GetNextSequenceAsync<TEntity>() where TEntity : class, IDBEntity, IDBFSequence, new();
}