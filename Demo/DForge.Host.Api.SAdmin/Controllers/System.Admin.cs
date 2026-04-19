namespace DForge.Host.Api.SAdmin.Controllers;

/// <summary>
/// 管理员
/// </summary>
public class SysAdminController : DomainForgeController
{
    private IAdminService<HTTPTokenProvider> _service = InjectionContext.ResolveByKeyed<IAdminService<HTTPTokenProvider>>(GlobalPermissionEnum.SAdmin);

    /// <summary>
    /// 创建
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost, Route("Create")]
    public async Task<IApiResult<bool>> Create(DTOSysAdminCreate input) => (await _service.Create(input)).ToMLApiResult();

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
    public async Task<IApiResult<bool>> Update(DTOSysAdminUpdate input) => (await _service.Update(input)).ToMLApiResult();

    /// <summary>
    /// 状态变更
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost, Route("Status")]
    public async Task<IApiResult<bool>> Status(DTOSysAdminStatus input) => (await _service.Status(input)).ToMLApiResult();

    /// <summary>
    /// 重置密码
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost, Route("ResetPassword")]
    public async Task<IApiResult<bool>> ResetPassword(DTOPrimaryKeyRequired<string> input) => (await _service.ResetPassword(input)).ToMLApiResult();

    /// <summary>
    /// 重置MFA
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost, Route("ResetMFA")]
    public async Task<IApiResult<string>> ResetMFA(DTOPrimaryKeyRequired<string> input) => (await _service.ResetMFA(input)).ToMLApiResult();

    /// <summary>
    /// 详情
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost, Route("Single")]
    public async Task<IApiResult<DTOSysAdminSingleResult>> Single(DTOPrimaryKeyRequired<string> input) => (await _service.Single(input)).ToMLApiResult();

    /// <summary>
    /// 分页
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost, Route("Page")]
    public async Task<IApiResult<DTOPageObj<DTOSysAdminPageResult>>> Page(DTOSysAdminPage input) => (await _service.Page(input)).ToMLApiResult();
}