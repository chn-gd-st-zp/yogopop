namespace DForge.Host.Api.SAdmin.Controllers;

/// <summary>
/// 菜单
/// </summary>
public class SysMenuController : DomainForgeController
{
    private ISysMenuService<HTTPTokenProvider> _service = InjectionContext.ResolveByKeyed<ISysMenuService<HTTPTokenProvider>>(GlobalPermissionEnum.SAdmin);

    /// <summary>
    /// 创建
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost, Route("Create")]
    public async Task<IApiResult<string>> Create(DTOSysMenuCreate input) => (await _service.Create(input)).ToMLApiResult();

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpDelete, Route("Delete")]
    public async Task<IApiResult<bool>> Delete(DTOPrimaryKeyRequired<string> input) => (await _service.Delete(input)).ToMLApiResult();

    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost, Route("Update")]
    public async Task<IApiResult<bool>> Update(DTOSysMenuUpdate input) => (await _service.Update(input)).ToMLApiResult();

    /// <summary>
    /// 排序，与目标调换位置
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost, Route("Sort")]
    public async Task<IApiResult<bool>> Sort(DTOSysMenuSort input) => (await _service.Sort(input)).ToMLApiResult();

    /// <summary>
    /// 树形
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost, Route("Tree")]
    public async Task<IApiResult<DTOTree<DTOSysMenuTreeResult>>> Tree(DTOSysMenuTree input) => (await _service.Tree(input)).ToMLApiResult();
}