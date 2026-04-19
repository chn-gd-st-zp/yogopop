namespace DForge.Contract.SAdmin;

public interface ISysRoleService<TTokenProvider> : ITransient where TTokenProvider : ITokenProvider
{
    public Task<IServiceResult<bool>> Create(DTOSysRoleCreate input);

    public Task<IServiceResult<bool>> Delete(DTOPrimaryKeyRequired<string> input);

    public Task<IServiceResult<bool>> Update(DTOSysRoleUpdate input);

    public Task<IServiceResult<bool>> Status(DTOSysRoleStatus input);

    public Task<IServiceResult<DTOSysRoleSingleResult>> Single(DTOPrimaryKeyRequired<string> input);

    public Task<IServiceResult<DTOPageObj<DTOSysRolePageResult>>> Page(DTOSysRolePage input);
}