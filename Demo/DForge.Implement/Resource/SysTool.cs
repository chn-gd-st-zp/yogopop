namespace DForge.Implement.Resource;

[DIModeForService(DIModeEnum.Exclusive, typeof(ISysToolService<>))]
public partial class SysStatusService<TTokenProvider> : ApiResourceService<KVResourceService<TTokenProvider>, ICache, TTokenProvider>, ISysToolService<TTokenProvider> where TTokenProvider : ITokenProvider
{
    public async Task<IServiceResult<DTOSystemStatusGetResult>> StatusGet() => (await SysStatusManager.GetAsync()).Success<DTOSystemStatusGetResult, LogicSucceed>();

    [ActionPermission(GlobalPermissionEnum.SysTool_StatusSet, GlobalPermissionEnum.SysTool)]
    public async Task<IServiceResult<bool>> StatusSet(DTOSystemStatusSet input) => await SysStatusManager.SetAsync(Logger, input);
}