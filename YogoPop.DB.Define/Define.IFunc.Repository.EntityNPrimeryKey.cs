namespace YogoPop.DB.Define;

public partial interface IDBRepositoryFunc<TEntity, TPrimaryKey> : IDBRepositoryFunc<TEntity> where TEntity : class, IDBEntity, IDBFPrimaryKey<TPrimaryKey>, new()
{
    //
}