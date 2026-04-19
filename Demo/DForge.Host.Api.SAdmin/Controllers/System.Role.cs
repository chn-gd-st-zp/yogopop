namespace DForge.Host.Api.SAdmin.Controllers;

/// <summary>
/// 角色
/// </summary>
public class SysRoleController : DomainForgeController
{
    private ISysRoleService<HTTPTokenProvider> _service = InjectionContext.ResolveByKeyed<ISysRoleService<HTTPTokenProvider>>(GlobalPermissionEnum.SAdmin);

    /// <summary>
    /// 创建
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost, Route("Create")]
    public async Task<IApiResult<bool>> Create(DTOSysRoleCreate input) => (await _service.Create(input)).ToMLApiResult();

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
    public async Task<IApiResult<bool>> Update(DTOSysRoleUpdate input) => (await _service.Update(input)).ToMLApiResult();

    /// <summary>
    /// 状态变更
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost, Route("Status")]
    public async Task<IApiResult<bool>> Status(DTOSysRoleStatus input) => (await _service.Status(input)).ToMLApiResult();

    /// <summary>
    /// 详情
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost, Route("Single")]
    public async Task<IApiResult<DTOSysRoleSingleResult>> Single(DTOPrimaryKeyRequired<string> input) => (await _service.Single(input)).ToMLApiResult();

    /// <summary>
    /// 分页
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost, Route("Page")]
    public async Task<IApiResult<DTOPageObj<DTOSysRolePageResult>>> Page(DTOSysRolePage input) => (await _service.Page(input)).ToMLApiResult();
}