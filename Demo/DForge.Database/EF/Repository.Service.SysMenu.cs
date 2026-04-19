namespace DForge.Database.EF;

[DIModeForService(DIModeEnum.Exclusive, typeof(ISysMenuRepository))]
public partial class SysMenuRepository : RenewEFDBRepository<TBSysMenu, string>, ISysMenuRepository
{
    public async Task<DTOTree<DTOSysMenuTreeResult>> TreeAsync(SysCategoryEnum sysCategory)
    {
        var dataList = new List<DTOSysMenuTreeResult>();

        using (var repository = InjectionContext.Resolve<ISysMenuRepository>())
        {
            var menuList = await repository.ListAsync(o => o.Category == sysCategory);
            if (menuList.IsNotEmpty())
                menuList.OrderBy(o => o.FullSequence).ToList().ForEach(o => { dataList.Add(o.MapTo<DTOSysMenuTreeResult>()); });
        }

        return dataList.ToTree(string.Empty);
    }
}