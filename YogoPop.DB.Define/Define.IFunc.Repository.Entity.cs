namespace YogoPop.DB.Define;

public partial interface IDBRepositoryFunc<TEntity> : IDBRepositoryFunc where TEntity : class, IDBEntity, new()
{
    //
}