namespace DForge.Database.EF;

[DIModeForService(DIModeEnum.Exclusive, typeof(ISysPermissionRepository))]
public partial class SysPermissionRepository : RenewEFDBRepository<TBSysPermission, string>, ISysPermissionRepository
{
    public List<TBSysPermission> ListByCodes(string[] codes)
    {
        var query = DBContext.GetQueryable<TBSysPermission>();

        var result = query.Where(o => codes.Contains(o.CurNode))
            .ToList();

        return result;
    }

    public List<TBSysPermission> ListByRoleCodes(string[] roleCodes)
    {
        var query_p = DBContext.GetQueryable<TBSysPermission>().AsQueryable();
        var query_rp = DBContext.GetQueryable<TBSysRolePermission>().AsQueryable();

        if (roleCodes != null && roleCodes.Length != 0)
            query_rp = query_rp.Where(o => roleCodes.Contains(o.RoleCode));

        var result = query_rp
            .GroupJoin(query_p, l => l.PermissionCode, r => r.CurNode, (l, r) => new { rp = l, r })
            .SelectMany(o => o.r.DefaultIfEmpty(), (l, r) => new { l.rp, p = r })
            .Select(o => o.p)
            .Where(o => o != null)
            .ToList();

        result = result.GroupBy(o => o.CurNode)
            .Select(o => o.FirstOrDefault())
            .Where(o => o != null)
            .ToList();

        return result;
    }

    public List<TBSysRolePermission> ListRolePermissionsByCodes(string roleCode, string[] permissionCodes)
    {
        var query = DBContext.GetQueryable<TBSysRolePermission>();

        var result = query.Where(o => o.RoleCode == roleCode && permissionCodes.Contains(o.PermissionCode))
            .ToList();

        return result;
    }
}