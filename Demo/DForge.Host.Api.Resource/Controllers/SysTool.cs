namespace DForge.Host.Api.Resource.Controllers;

/// <summary>
/// 系统工具
/// </summary>
public class SysToolController : DomainForgeController
{
    private ISysToolService<HTTPTokenProvider> _service = InjectionContext.Resolve<ISysToolService<HTTPTokenProvider>>();

    /// <summary>
    /// 维护状态 - 切换
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost, Route("StatusSet")]
    public async Task<IApiResult<bool>> StatusSet(DTOSystemStatusSet input) => (await _service.StatusSet(input)).ToMLApiResult();

    /// <summary>
    /// 维护状态 - 获取
    /// </summary>
    /// <returns></returns>
    [HttpGet, Route("StatusGet")]
    public async Task<IApiResult<DTOSystemStatusGetResult>> StatusGet() => (await _service.StatusGet()).ToMLApiResult();
}