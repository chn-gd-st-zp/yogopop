namespace DForge.Infrastructure.DBSupport.Repository;

public partial interface ISysMenuRepository : IDBRepository<TBSysMenu, string>, ITransient
{
    public Task<DTOTree<DTOSysMenuTreeResult>> TreeAsync(SysCategoryEnum sysCategory);
}