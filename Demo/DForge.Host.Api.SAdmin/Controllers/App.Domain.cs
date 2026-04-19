namespace DForge.Host.Api.SAdmin.Controllers;

/// <summary>
/// 域名
/// </summary>
public class AppDomainController : DomainForgeController
{
    private IAppDomainService<HTTPTokenProvider> _service = InjectionContext.ResolveByKeyed<IAppDomainService<HTTPTokenProvider>>(GlobalPermissionEnum.SAdmin);

    /// <summary>
    /// 分页
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost, Route("Page")]
    public async Task<IApiResult<DTOPageObj<DTOAppDomainPageResult>>> Page(DTOAppDomainPage input) => (await _service.Page(input)).ToMLApiResult();

    /// <summary>
    /// NS设置
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost, Route("NSSet")]
    public async Task<IApiResult<bool>> NSSet(DTOAppDomainNSSet input) => (await _service.Set(input)).ToMLApiResult();

    /// <summary>
    /// 同步发起
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost, Route("SyncApply")]
    public async Task<IApiResult<bool>> SyncApply(DTOAppDomainSyncCreate input) => (await _service.Create(input)).ToMLApiResult();

    /// <summary>
    /// 同步记录
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost, Route("SyncPage")]
    public async Task<IApiResult<DTOPageObj<DTOAppDomainSyncPageResult>>> SyncPage(DTOAppDomainSyncPage input) => (await _service.Page(input)).ToMLApiResult();
}