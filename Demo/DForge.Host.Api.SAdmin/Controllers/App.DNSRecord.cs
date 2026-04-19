namespace DForge.Host.Api.SAdmin.Controllers;

/// <summary>
/// DNS解析
/// </summary>
public class AppDNSRecordController : DomainForgeController
{
    private IAppDNSRecordService<HTTPTokenProvider> _service = InjectionContext.ResolveByKeyed<IAppDNSRecordService<HTTPTokenProvider>>(GlobalPermissionEnum.SAdmin);

    /// <summary>
    /// 列表
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost, Route("List")]
    public async Task<IApiResult<List<DTOAppDNSRecordListResult>>> List(DTOAppDNSRecordList input) => (await _service.List(input)).ToMLApiResult();

    /// <summary>
    /// DNS设置
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost, Route("DNSSet")]
    public async Task<IApiResult<bool>> DNSSet(DTOAppDNSRecordSet input) => (await _service.Set(input)).ToMLApiResult();
}