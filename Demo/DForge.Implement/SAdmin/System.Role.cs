namespace DForge.Implement.SAdmin;

[DIModeForService(DIModeEnum.ExclusiveByKeyed, typeof(Contract.SAdmin.ISysRoleService<>), GlobalPermissionEnum.SAdmin)]
public partial class SysRoleService<TTokenProvider> : ApiSAdminService<SysRoleService<TTokenProvider>, ICache, TTokenProvider>, Contract.SAdmin.ISysRoleService<TTokenProvider> where TTokenProvider : ITokenProvider
{
    [ActionPermission(GlobalPermissionEnum.Role_Create, GlobalPermissionEnum.Role, typeof(DTOSysRoleCreate), typeof(TBSysRole))]
    public async Task<IServiceResult<bool>> Create(DTOSysRoleCreate input)
    {
        var result = false;

        var code = $"{input.CurNode}";

        using (var sysPermissionRepository = InjectionContext.Resolve<ISysPermissionRepository>())
        using (var repository = InjectionContext.Resolve<ISysRoleRepository>())
        {
            //获取当前登录账号的所有角色中，等级最高的那个
            var curRole = await repository.GetHighestRoleAsync(Session.CurrentAccount.AccountInfo.RoleCodes);
            if (curRole == null || curRole.Status != StatusEnum.Normal)
                return result.Fail<bool, RoleTypeInvalid>();

            if (await repository.AnyAsync(o => o.CurNode == code))
                return result.Fail<bool, RoleDuplicated>();

            if (input.Type.In(RoleEnum.SuperAdmin)) return result.Fail<bool, RoleOverLimit>();
            else if (input.Type.In(RoleEnum.Admin)) input.SubType = UserTypeEnum.None;
            else if (input.Type.In(RoleEnum.User))
            {
                if (input.SubType.IsNullOrDefault()) input.SubType = UserTypeEnum.Normal;
                if (await repository.AnyAsync(o => o.Type == input.Type && o.SubType == input.SubType && o.GroupID == string.Empty))
                    return result.Fail<bool, RoleOverLimit>();
            }

            var obj = input.MapTo<TBSysRole>();
            obj.CurNode = code;
            obj.Level = curRole.Level + 1;
            obj.ParentNode = curRole.CurNode;
            obj.FullNode = curRole.FullNode + obj.CurNode;
            obj.CreateTime = DateTimeExtension.Now;

            var obj_permissions_c = new List<TBSysRolePermission>();

            if (input.PermissionCodes_Create.IsNotEmpty())
            {
                foreach (var permission in sysPermissionRepository.ListByCodes(input.PermissionCodes_Create))
                {
                    obj_permissions_c.Add(new TBSysRolePermission
                    {
                        RoleCode = obj.CurNode,
                        PermissionCode = permission.CurNode,
                    });
                }
            }

            result = await repository.CreateAsync(obj, obj_permissions_c);
            if (!result)
                return result.Fail<bool, LogicFailed>();
        }

        return result.Success<bool, LogicSucceed>();
    }

    [ActionPermission(GlobalPermissionEnum.Role_Delete, GlobalPermissionEnum.Role, typeof(DTOPrimaryKeyRequired<string>), typeof(TBSysRole))]
    public async Task<IServiceResult<bool>> Delete(DTOPrimaryKeyRequired<string> input)
    {
        var result = false;

        using (var repository = InjectionContext.Resolve<ISysRoleRepository>())
        {
            var obj = await repository.SingleAsync(input.PrimaryKey);
            if (obj == null)
                return result.Fail<bool, RoleNotFound>();

            if (obj.Type == RoleEnum.SuperAdmin)
                return result.Fail<bool, RoleTypeInvalid>();

            obj.Status = StatusEnum.Delete;

            result = await repository.UpdateAsync(obj);
            if (!result)
                return result.Fail<bool, LogicFailed>();
        }

        return result.Success<bool, LogicSucceed>();
    }

    [ActionPermission(GlobalPermissionEnum.Role_Update, GlobalPermissionEnum.Role, typeof(DTOSysRoleUpdate), typeof(TBSysRole))]
    public async Task<IServiceResult<bool>> Update(DTOSysRoleUpdate input)
    {
        var result = false;

        using (var sysPermissionRepository = InjectionContext.Resolve<ISysPermissionRepository>())
        using (var repository = InjectionContext.Resolve<ISysRoleRepository>())
        {
            var obj = await repository.SingleAsync(input.PrimaryKey);
            if (obj == null)
                return result.Fail<bool, RoleNotFound>();

            if (obj.Type == RoleEnum.SuperAdmin)
                return result.Fail<bool, RoleTypeInvalid>();

            obj.Name = input.Name;
            obj.Status = input.Status;

            var obj_permissions_c = new List<TBSysRolePermission>();

            if (input.PermissionCodes_Create.IsNotEmpty())
            {
                foreach (var permission in sysPermissionRepository.ListByCodes(input.PermissionCodes_Create))
                {
                    obj_permissions_c.Add(new TBSysRolePermission
                    {
                        RoleCode = obj.CurNode,
                        PermissionCode = permission.CurNode,
                    });
                }
            }

            var obj_permissions_d = new List<TBSysRolePermission>();

            if (input.PermissionCodes_Delete.IsNotEmpty())
            {
                foreach (var permission in sysPermissionRepository.ListRolePermissionsByCodes(obj.CurNode, input.PermissionCodes_Delete))
                {
                    obj_permissions_d.Add(permission);
                }
            }

            result = await repository.UpdateAsync(obj, obj_permissions_c, obj_permissions_d);
            if (!result)
                return result.Fail<bool, LogicFailed>();
        }

        return result.Success<bool, LogicSucceed>();
    }

    [ActionPermission(GlobalPermissionEnum.Role_Status, GlobalPermissionEnum.Role, typeof(DTOSysRoleStatus), typeof(TBSysRole))]
    public async Task<IServiceResult<bool>> Status(DTOSysRoleStatus input)
    {
        var result = false;

        using (var repository = InjectionContext.Resolve<ISysRoleRepository>())
        {
            var obj = await repository.SingleAsync(input.PrimaryKey);
            if (obj == null)
                return result.Fail<bool, RoleNotFound>();

            obj.Status = input.Status;

            result = await repository.UpdateAsync(obj);
            if (!result)
                return result.Fail<bool, LogicFailed>();
        }

        return result.Success<bool, LogicSucceed>();
    }

    [ActionPermission(GlobalPermissionEnum.Role_Single, GlobalPermissionEnum.Role)]
    public async Task<IServiceResult<DTOSysRoleSingleResult>> Single(DTOPrimaryKeyRequired<string> input)
    {
        var result = default(DTOSysRoleSingleResult);

        using (var repository = InjectionContext.Resolve<ISysPermissionRepository>())
        {
            var obj = await repository.DBContext.SingleAsync<TBSysRole>(o => o.PrimaryKey == input.PrimaryKey);
            if (obj == null)
                return result.Fail<DTOSysRoleSingleResult, RoleNotFound>();

            var pers = repository.ListByRoleCodes(new string[] { obj.CurNode });

            result = obj.MapTo<DTOSysRoleSingleResult>();

            result.PermissionCodes = pers.Select(o => o.CurNode).ToArray();
        }

        return result.Success<DTOSysRoleSingleResult, LogicSucceed>();
    }

    [ActionPermission(GlobalPermissionEnum.Role_Page, GlobalPermissionEnum.Role)]
    public async Task<IServiceResult<DTOPageObj<DTOSysRolePageResult>>> Page(DTOSysRolePage input)
    {
        var result = default(DTOPageObj<DTOSysRolePageResult>);

        using (var repository = InjectionContext.Resolve<ISysRoleRepository>())
            result = (await repository.PageAsync(input)).ToDTOPageObj(input, ep => ep.MapTo<DTOSysRolePageResult>());

        return result.Success<DTOPageObj<DTOSysRolePageResult>, LogicSucceed>();
    }
}