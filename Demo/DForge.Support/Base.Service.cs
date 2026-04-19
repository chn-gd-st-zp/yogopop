namespace DForge.Support;

public abstract class DomainForgeService<TService, TCache> : ServiceBase<TService, TCache>
    where TCache : ICache
    where TService : class, IYogoService
{
    protected readonly SystemSettings SystemSettings;
    protected readonly IdenHelper IdenHelper;
    protected IHttpContextAccessor HttpContextAccessor;
    protected IHeaderDictionary RequestHeaders => HttpContextAccessor.HttpContext.Request.Headers;

    public DomainForgeService()
    {
        SystemSettings = InjectionContext.Resolve<SystemSettings>();
        IdenHelper = InjectionContext.Resolve<IdenHelper>();
        HttpContextAccessor = InjectionContext.Resolve<IHttpContextAccessor>();
    }
}

[GroupPermission(GlobalPermissionEnum.SystemPermission)]
[GroupPermission(GlobalPermissionEnum.Base, GlobalPermissionEnum.SystemPermission)]
[GroupPermission(GlobalPermissionEnum.SAdmin, GlobalPermissionEnum.SystemPermission)]
public abstract class DomainForgeService<TService, TCache, TTokenProvider> : ServiceBase<TService, TCache, TTokenProvider>
    where TCache : ICache
    where TService : class, IYogoService
    where TTokenProvider : ITokenProvider
{
    protected readonly SystemSettings SystemSettings;
    protected readonly IdenHelper IdenHelper;
    protected IHttpContextAccessor HttpContextAccessor;
    protected IHeaderDictionary RequestHeaders => HttpContextAccessor.HttpContext.Request.Headers;

    public DomainForgeService()
    {
        SystemSettings = InjectionContext.Resolve<SystemSettings>();
        IdenHelper = InjectionContext.Resolve<IdenHelper>();
        HttpContextAccessor = InjectionContext.Resolve<IHttpContextAccessor>();
    }

    protected virtual string CurAccountID => Session.CurrentAccount.AccountInfo.AccountID;

    protected SysStatusEnum SysStatus
    {
        get
        {
            var sysStatus = SysStatusManager.GetAsync().GetAwaiter().GetResult();
            return sysStatus != null ? sysStatus.Status : SysStatusEnum.Maintaining;
        }
    }

    /// <summary>
    /// 获取权限的情景
    /// 1. 创建角色的时候：当前登录的账号的角色的总权限集合，即为新角色的总权限集合
    /// 2. 修改角色的时候：当前编辑的角色的总权限集合，则为该角色的总权限集合
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="input"></param>
    /// <returns></returns>
    /// <exception cref="VEParamsValidation"></exception>
    protected async Task<List<TResult>> GetPermissions<TResult>(DTOSysPermissionSearch input) where TResult : DTOSysPermissionResult
    {
        var result = new List<TResult>();

        var roleCodes = new List<string>();

        if (input.RoleCodes.IsEmpty())
        {
            //无传递参数，视为不指定角色，以当前登录角色为基准
            roleCodes.AddRange(Session.CurrentAccount.AccountInfo.RoleCodes);
        }
        else
        {
            //有传递参数，视为要指定角色，以参数为基准
            roleCodes.AddRange(input.RoleCodes);
        }

        //去重
        roleCodes = roleCodes.GroupBy(o => o).Select(o => o.First()).ToList();

        using (var sysRoleRepository = InjectionContext.Resolve<ISysRoleRepository>())
        using (var sysPermissionRepository = InjectionContext.Resolve<ISysPermissionRepository>())
        {
            //所有要查询权限的角色，必须全都是前登录账号所属的角色的子孙
            foreach (var role in await sysRoleRepository.ListByCodesAsync(roleCodes.ToArray()))
            {
                bool hasExtendItem = input.CurrentAccountRoleCodes != null && input.CurrentAccountRoleCodes.Any() ?
                    input.CurrentAccountRoleCodes.Where(o => role.FullNode.Contains($",{o},")).Any()
                    :
                    Session.CurrentAccount.AccountInfo.RoleCodes.Where(o => role.FullNode.Contains($",{o},")).Any();

                if (!hasExtendItem)
                    throw new VEParamsValidation("所要查询权限的角色中，存在不合法的角色");
            }

            //获取权限
            (sysPermissionRepository.ListByRoleCodes(roleCodes.ToArray()))
                .OrderBy(o => o.FullSequence)
                .ToList()
                .ForEach(o => { result.Add(o.MapTo<TResult>()); });
        }

        return result;
    }
}