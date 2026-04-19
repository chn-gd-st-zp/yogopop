namespace DForge.Implement.SAdmin;

[DIModeForService(DIModeEnum.ExclusiveByKeyed, typeof(Contract.SAdmin.ISysPermissionService<>), GlobalPermissionEnum.SAdmin)]
public partial class SysPermissionService<TTokenProvider> : ApiSAdminService<SysPermissionService<TTokenProvider>, ICache, TTokenProvider>, Contract.SAdmin.ISysPermissionService<TTokenProvider> where TTokenProvider : ITokenProvider
{
    public async Task<IServiceResult<List<DTOSysPermissionListResult>>> List(DTOSysPermissionList input)
    {
        input.CurrentAccountRoleCodes = Session.CurrentAccount.AccountInfo.RoleCodes;
        var result = await GetPermissions<DTOSysPermissionListResult>(input);

        return result.Success<List<DTOSysPermissionListResult>, LogicSucceed>();
    }

    public async Task<IServiceResult<DTOTree<DTOSysPermissionTreeResult>>> Tree(DTOSysPermissionTree input)
    {
        input.CurrentAccountRoleCodes = Session.CurrentAccount.AccountInfo.RoleCodes;
        var dataList = await GetPermissions<DTOSysPermissionTreeResult>(input);

        return dataList.ToTree(string.Empty).Success<DTOTree<DTOSysPermissionTreeResult>, LogicSucceed>();
    }

    [ActionPermission(GlobalPermissionEnum.Permission_Update, GlobalPermissionEnum.Permission, typeof(DTOSysPermissionUpdate), typeof(TBSysPermission))]
    public async Task<IServiceResult<bool>> Update(DTOSysPermissionUpdate input)
    {
        var result = default(bool);

        using (var repository = InjectionContext.Resolve<ISysPermissionRepository>())
        {
            var obj = await repository.SingleAsync(input.PrimaryKey);
            if (obj == null)
                return result.Fail<bool, TargetNotFound>();

            obj.AccessLogger = input.AccessLogger;

            result = await repository.UpdateAsync(obj);
            if (!result)
                return result.Fail<bool, LogicFailed>();
        }

        return result.Success<bool, LogicSucceed>();
    }
}