namespace DForge.Contract.SAdmin;

public interface ISysMenuService<TTokenProvider> : ITransient where TTokenProvider : ITokenProvider
{
    public Task<IServiceResult<string>> Create(DTOSysMenuCreate input);

    public Task<IServiceResult<bool>> Delete(DTOPrimaryKeyRequired<string> input);

    public Task<IServiceResult<bool>> Update(DTOSysMenuUpdate input);

    public Task<IServiceResult<bool>> Sort(DTOSysMenuSort input);

    public Task<IServiceResult<DTOTree<DTOSysMenuTreeResult>>> Tree(DTOSysMenuTree input);
}