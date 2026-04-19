namespace DForge.Implement.SAdmin;

[DIModeForService(DIModeEnum.ExclusiveByKeyed, typeof(Contract.SAdmin.ISysMenuService<>), GlobalPermissionEnum.SAdmin)]
public partial class SysMenuService<TTokenProvider> : ApiSAdminService<SysMenuService<TTokenProvider>, ICache, TTokenProvider>, Contract.SAdmin.ISysMenuService<TTokenProvider> where TTokenProvider : ITokenProvider
{
    [ActionPermission(GlobalPermissionEnum.Menu_Create, GlobalPermissionEnum.Menu, typeof(DTOSysMenuCreate), typeof(TBSysMenu))]
    public async Task<IServiceResult<string>> Create(DTOSysMenuCreate input)
    {
        var result = string.Empty;

        var code = Unique.GetRandomCode4(8);

        using (var repository = InjectionContext.Resolve<ISysMenuRepository>())
        {
            var parentNode = await repository.SingleAsync(o => o.CurNode == input.ParentNode);
            if (parentNode == null) return result.Fail<string, TargetNotFound>();

            if (await repository.AnyAsync(o => o.CurNode == code)) return result.Fail<string, LogicFailed>();

            var obj = input.MapTo<TBSysMenu>();
            obj.CurNode = code;
            obj.ParentNode = parentNode.CurNode;
            obj.SetFullNode<TBSysMenu, string>(parentNode.FullNode);
            obj.SetSequence(repository.DBContext.GetNextSequence<TBSysMenu>());
            obj.SetFullSequence<TBSysMenu, string>(parentNode.FullSequence);

            if (!await repository.CreateAsync(obj))
                return result.Fail<string, LogicFailed>();

            result = obj.PrimaryKey;
        }

        return result.Success<string, LogicSucceed>();
    }

    [ActionPermission(GlobalPermissionEnum.Menu_Delete, GlobalPermissionEnum.Menu, typeof(DTOPrimaryKeyRequired<string>), typeof(TBSysMenu))]
    public async Task<IServiceResult<bool>> Delete(DTOPrimaryKeyRequired<string> input)
    {
        var result = false;

        using (var repository = InjectionContext.Resolve<ISysMenuRepository>())
        {
            var obj = await repository.SingleAsync(input.PrimaryKey);
            if (obj == null) return result.Fail<bool, TargetNotFound>();

            if (obj.Type == SysMenuEnum.None) return result.Fail<bool, LogicFailed>();

            obj.Status = StatusEnum.Delete;

            result = await repository.UpdateAsync(obj);
            if (!result)
                return result.Fail<bool, LogicFailed>();
        }

        return result.Success<bool, LogicSucceed>();
    }

    [ActionPermission(GlobalPermissionEnum.Menu_Update, GlobalPermissionEnum.Menu, typeof(DTOSysMenuUpdate), typeof(TBSysMenu))]
    public async Task<IServiceResult<bool>> Update(DTOSysMenuUpdate input)
    {
        var result = false;

        using (var repository = InjectionContext.Resolve<ISysMenuRepository>())
        {
            var obj = await repository.SingleAsync(input.PrimaryKey);
            if (obj == null) return result.Fail<bool, TargetNotFound>();

            if (obj.Type == SysMenuEnum.None) return result.Fail<bool, LogicFailed>();

            obj = input.AdaptTo(obj);

            result = await repository.UpdateAsync(obj);
            if (!result)
                return result.Fail<bool, LogicFailed>();
        }

        return result.Success<bool, LogicSucceed>();
    }

    [ActionPermission(GlobalPermissionEnum.Menu_Sort, GlobalPermissionEnum.Menu, typeof(DTOSysMenuSort), typeof(TBSysMenu))]
    public async Task<IServiceResult<bool>> Sort(DTOSysMenuSort input)
    {
        var result = false;

        using (var repository = InjectionContext.Resolve<ISysMenuRepository>())
        {
            var source = await repository.SingleAsync(input.SourceID);
            if (source == null) return result.Fail<bool, TargetNotFound>();

            var target = await repository.SingleAsync(input.TargetID);
            if (target == null) return result.Fail<bool, TargetNotFound>();

            if (source.Type == SysMenuEnum.None || target.Type == SysMenuEnum.None) return result.Fail<bool, LogicFailed>();

            var curSequence = source.CurSequence;
            var fullSequence = source.FullSequence;

            source.CurSequence = target.CurSequence;
            source.FullSequence = target.FullSequence;
            target.CurSequence = curSequence;
            target.FullSequence = fullSequence;

            result = await repository.UpdateAsync(new List<TBSysMenu> { source, target });
            if (!result)
                return result.Fail<bool, LogicFailed>();
        }

        return result.Success<bool, LogicSucceed>();
    }

    public async Task<IServiceResult<DTOTree<DTOSysMenuTreeResult>>> Tree(DTOSysMenuTree input)
    {
        var result = default(DTOTree<DTOSysMenuTreeResult>);

        using (var repository = InjectionContext.Resolve<ISysMenuRepository>())
            result = await repository.TreeAsync(input.Category);

        return result.Success<DTOTree<DTOSysMenuTreeResult>, LogicSucceed>();
    }
}