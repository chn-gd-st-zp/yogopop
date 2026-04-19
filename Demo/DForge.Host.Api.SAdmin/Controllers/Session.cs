namespace DForge.Host.Api.SAdmin.Controllers;

/// <summary>
/// 会话
/// </summary>
public class SessionController : DomainForgeController
{
    private ISessionService<HTTPTokenProvider> _service = InjectionContext.ResolveByKeyed<ISessionService<HTTPTokenProvider>>(GlobalPermissionEnum.SAdmin);

    /// <summary>
    /// 登录
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpPost, Route("SignIn")]
    public async Task<IApiResult<DTOSessionSAdminSignInResult>> SignIn(DTOSessionSAdminSignIn input) => (await _service.SignIn(input)).ToMLApiResult();

    /// <summary>
    /// 登出
    /// </summary>
    /// <returns></returns>
    [HttpPost, Route("SignOut")]
    public async Task<IApiResult<bool>> SignOut() => (await _service.SignOut()).ToMLApiResult();

    /// <summary>
    /// 获取
    /// </summary>
    /// <returns></returns>
    [HttpGet, Route("Get")]
    public async Task<IApiResult<DTOSessionSAdminSignInResult>> Get() => (await _service.Get()).ToMLApiResult();

    /// <summary>
    /// 修改密码
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost, Route("UpdatePassword")]
    public async Task<IApiResult<bool>> UpdatePassword(DTOSessionUpdatePassword input) => (await _service.UpdatePassword(input)).ToMLApiResult();

    /// <summary>
    /// 分页
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost, Route("Page")]
    public async Task<IApiResult<DTOPageObj<DTOSessionPageResult>>> Page(DTOSessionPage input) => (await _service.Page(input)).ToMLApiResult();

    /// <summary>
    /// 踢除
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost, Route("Kick")]
    public async Task<IApiResult<bool>> Kick(DTOSessionKick input) => (await _service.Kick(input)).ToMLApiResult();
}