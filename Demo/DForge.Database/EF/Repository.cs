namespace DForge.Database.EF;

[DIModeForService(DIModeEnum.Exclusive, typeof(IDBRepository))]
public class RenewEFDBRepository : EFDBRepository, ITransient
{
    //
}

[DIModeForService(DIModeEnum.Exclusive, typeof(IDBRepository<>))]
public class RenewEFDBRepository<TEntity> : EFDBRepository<TEntity>, ITransient
   where TEntity : class, IDBEntity, new()
{
    //
}

//[DIModeForService(DIModeEnum.Exclusive, typeof(IDBRepository<,>))]
//public class RenewEFDBRepository<TEntity, TKey> : EFDBRepository<TEntity, TKey>, ITransient
//   where TEntity : class, IDBEntity, IDBFPrimaryKey<TKey>, new()
//{
//    //
//}

[DIModeForService(DIModeEnum.Exclusive, typeof(IDBRepository<,>))]
public class RenewEFDBRepository<TEntity, TKey> : EFDBRepository<DomainForgeEFDBContext, TEntity, TKey>, ITransient
   where TEntity : class, IDBEntity, IDBFPrimaryKey<TKey>, new()
{
    //
}