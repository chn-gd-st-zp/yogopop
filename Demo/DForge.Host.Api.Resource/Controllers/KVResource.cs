namespace DForge.Host.Api.Resource.Controllers;

/// <summary>
/// 键值资源
/// </summary>
public class KVResourceController : DomainForgeController
{
    private IKVResourceService<HTTPTokenProvider> _service = InjectionContext.Resolve<IKVResourceService<HTTPTokenProvider>>();

    /// <summary>
    /// 获取
    /// </summary>
    /// <returns></returns>
    [HttpGet, Route("Get")]
    public async Task<IApiResult<SortedDictionary<string, object>>> Get() => (await _service.ResourceData(GlobalSupport.CurLanguage)).ToMLApiResult();

    /// <summary>
    /// 刷新
    /// </summary>
    /// <returns></returns>
    [HttpGet, Route("Refresh")]
    public async Task<IApiResult<bool>> Refresh() => (await _service.Refresh()).ToMLApiResult();
}