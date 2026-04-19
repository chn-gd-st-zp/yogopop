namespace DForge.Database.EF;

[DIModeForService(DIModeEnum.Exclusive, typeof(IPermissionRepository))]
[DIModeForService(DIModeEnum.ExclusiveByKeyed, typeof(IAccessRecordTriggerRepository), typeof(TBSysPermission))]
public partial class SysPermissionRepository
{
    public bool Create(IEnumerable<IPermission> permissions)
    {
        var result = false;

        using (var tranScope = UnitOfWork.GenerateTransactionScope())
        {
            var permissions_all = permissions.Select(o => o as TBSysPermission).ToList();
            if (permissions_all.IsNotEmpty())
            {
                result = DBContext.Create(permissions_all, false);
                if (!result) return result;
            }

            if (permissions_all.Any(o => o.FullNode.Contains($",{GlobalPermissionEnum.SAdmin},")))
            {
                var role_sa = DBContext.List<TBSysRole>(o => o.Type == RoleEnum.SuperAdmin).FirstOrDefault();
                var rolePermissions_sa = permissions_all
                    .Select(o => new TBSysRolePermission
                    {
                        RoleCode = role_sa.CurNode,
                        PermissionCode = o.CurNode,
                    })
                    .ToList();

                if (rolePermissions_sa.IsNotEmpty())
                {
                    result = DBContext.Create(rolePermissions_sa, false);
                    if (!result) return result;
                }
            }

            DBContext.SaveChanges();
            tranScope.Complete();
        }

        return result;
    }

    public bool Delete(IEnumerable<IPermission> permissions)
    {
        var result = false;

        using (var tranScope = UnitOfWork.GenerateTransactionScope())
        {
            var permissions_all = permissions.Select(o => o as TBSysPermission).ToList();
            if (permissions_all.IsNotEmpty())
            {
                result = DBContext.Delete(permissions_all, false);
                if (!result) return result;
            }

            if (permissions_all.Any(o => o.FullNode.Contains($",{GlobalPermissionEnum.SAdmin},")))
            {
                var role_sa = DBContext.List<TBSysRole>(o => o.Type == RoleEnum.SuperAdmin).FirstOrDefault();
                var rolePermissions_sa = DBContext.List<TBSysRolePermission>(o => o.RoleCode == role_sa.CurNode);
                if (rolePermissions_sa.IsNotEmpty())
                {
                    result = DBContext.Delete(rolePermissions_sa, false);
                    if (!result) return result;
                }
            }

            DBContext.SaveChanges();
            tranScope.Complete();
        }

        return result;
    }

    public IPermission Permission(string code) => DBContext.Single<TBSysPermission>(o => o.CurNode == code);

    public IEnumerable<IPermission> AllPermission() => DBContext.List<TBSysPermission>().OrderBy(o => o.FullSequence).Select(o => o as IPermission).ToList();

    public IAccessRecordTrigger GetTriggerObj(object pk) => DBContext.Single<TBSysPermission>(o => o.PrimaryKey == pk.ToString());
}