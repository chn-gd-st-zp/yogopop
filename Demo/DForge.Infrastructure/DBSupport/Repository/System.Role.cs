namespace DForge.Infrastructure.DBSupport.Repository;

public partial interface ISysRoleRepository : IDBRepository<TBSysRole, string>, ITransient
{
    public Task<bool> CreateAsync(TBSysRole role, List<TBSysRolePermission> permissions);

    public Task<bool> UpdateAsync(TBSysRole role, List<TBSysRolePermission> permissions_c, List<TBSysRolePermission> permissions_d);

    public Task<TBSysRole> SingleByFirstUserTypeAsync();

    public Task<Tuple<List<TBSysRole>, int>> PageAsync(DTOSysRolePage input, string groupID = "");

    public Task<List<TBSysRole>> ListByCodesAsync(string[] roleCodes);

    public Task<TBSysRole> GetHighestRoleAsync(string[] roleCodes);
}