namespace DForge.Contract.SAdmin;

public interface ISysPermissionService<TTokenProvider> : ITransient where TTokenProvider : ITokenProvider
{
    public Task<IServiceResult<bool>> Update(DTOSysPermissionUpdate input);

    public Task<IServiceResult<List<DTOSysPermissionListResult>>> List(DTOSysPermissionList input);

    public Task<IServiceResult<DTOTree<DTOSysPermissionTreeResult>>> Tree(DTOSysPermissionTree input);
}