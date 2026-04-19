namespace DForge.Support;

[GroupPermission(GlobalPermissionEnum.AppManage, GlobalPermissionEnum.SAdmin)]
[GroupPermission(GlobalPermissionEnum.SysManage, GlobalPermissionEnum.SAdmin)]

[GroupPermission(GlobalPermissionEnum.SysTool, GlobalPermissionEnum.Base)]
[GroupPermission(GlobalPermissionEnum.Attachment, GlobalPermissionEnum.Base)]
[GroupPermission(GlobalPermissionEnum.Account_Property, GlobalPermissionEnum.Base)]
[GroupPermission(GlobalPermissionEnum.Account_Property_Create, GlobalPermissionEnum.Account_Property)]
[GroupPermission(GlobalPermissionEnum.Account_Property_Update, GlobalPermissionEnum.Account_Property)]
[GroupPermission(GlobalPermissionEnum.Account_Property_Search, GlobalPermissionEnum.Account_Property)]

[GroupPermission(GlobalPermissionEnum.Project, GlobalPermissionEnum.AppManage)]
[GroupPermission(GlobalPermissionEnum.DSPChannel, GlobalPermissionEnum.AppManage)]
[GroupPermission(GlobalPermissionEnum.Domain, GlobalPermissionEnum.AppManage)]
[GroupPermission(GlobalPermissionEnum.DNSRecord, GlobalPermissionEnum.AppManage)]

[GroupPermission(GlobalPermissionEnum.AccessRecord, GlobalPermissionEnum.SysManage)]
[GroupPermission(GlobalPermissionEnum.Permission, GlobalPermissionEnum.SysManage)]
[GroupPermission(GlobalPermissionEnum.Role, GlobalPermissionEnum.SysManage)]
[GroupPermission(GlobalPermissionEnum.Menu, GlobalPermissionEnum.SysManage)]
[GroupPermission(GlobalPermissionEnum.Admin, GlobalPermissionEnum.SysManage)]
[GroupPermission(GlobalPermissionEnum.Session, GlobalPermissionEnum.SysManage)]

public class ApiSAdminService<TService, TCache, TTokenProvider> : DomainForgeService<TService, TCache, TTokenProvider>
    where TCache : ICache
    where TService : class, IYogoService
    where TTokenProvider : ITokenProvider
{
    /// <summary>
    /// 获取管理界面左侧菜单
    /// </summary>
    /// <returns></returns>
    protected async Task<DTOTree<DTOSysMenuTreeResult>> Menus()
    {
        var result = default(DTOTree<DTOSysMenuTreeResult>);

        using (var repository = InjectionContext.Resolve<ISysMenuRepository>())
            result = await repository.TreeAsync(SysCategoryEnum.SAdmin);

        return result;
    }
}