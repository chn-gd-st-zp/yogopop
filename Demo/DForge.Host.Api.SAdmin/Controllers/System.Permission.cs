namespace DForge.Host.Api.SAdmin.Controllers;

/// <summary>
/// 权限
/// </summary>
public class SysPermissionController : DomainForgeController
{
    private ISysPermissionService<HTTPTokenProvider> _service = InjectionContext.ResolveByKeyed<ISysPermissionService<HTTPTokenProvider>>(GlobalPermissionEnum.SAdmin);

    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost, Route("Update")]
    public async Task<IApiResult<bool>> Update(DTOSysPermissionUpdate input) => (await _service.Update(input)).ToMLApiResult();

    /// <summary>
    /// 列表
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost, Route("List")]
    public async Task<IApiResult<List<DTOSysPermissionListResult>>> List(DTOSysPermissionList input) => (await _service.List(input)).ToMLApiResult();

    /// <summary>
    /// 树形
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost, Route("Tree")]
    public async Task<IApiResult<DTOTree<DTOSysPermissionTreeResult>>> Tree(DTOSysPermissionTree input) => (await _service.Tree(input)).ToMLApiResult();
}