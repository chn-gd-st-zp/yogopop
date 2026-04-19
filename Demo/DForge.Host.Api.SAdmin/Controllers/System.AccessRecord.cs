namespace DForge.Host.Api.SAdmin.Controllers;

/// <summary>
/// 系统日志
/// </summary>
public class SysAccessRecordController : DomainForgeController
{
    private ISysAccessRecordService<HTTPTokenProvider> _service = InjectionContext.ResolveByKeyed<ISysAccessRecordService<HTTPTokenProvider>>(GlobalPermissionEnum.SAdmin);

    /// <summary>
    /// 详情
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost, Route("Detail")]
    public async Task<IApiResult<DTOSysAccessRecordSingleResult>> Single(DTOSysAccessRecordSingle input) => (await _service.Single(input)).ToMLApiResult();

    /// <summary>
    /// 分页
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost, Route("Page")]
    public async Task<IApiResult<DTOPageObj<DTOSysAccessRecordPageResult>>> Page(DTOSysAccessRecordPage input) => (await _service.Page(input)).ToMLApiResult();
}