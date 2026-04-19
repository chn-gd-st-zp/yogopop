namespace DForge.Infrastructure.DBSupport.Repository;

public partial interface ISysPermissionRepository : IDBRepository<TBSysPermission, string>, ITransient
{
    public List<TBSysPermission> ListByCodes(string[] codes);

    public List<TBSysPermission> ListByRoleCodes(string[] roleCodes);

    public List<TBSysRolePermission> ListRolePermissionsByCodes(string roleCode, string[] permissionCodes);
}