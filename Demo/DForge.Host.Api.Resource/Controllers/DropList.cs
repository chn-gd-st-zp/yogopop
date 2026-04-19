namespace DForge.Host.Api.Resource.Controllers;

/// <summary>
/// 下拉框
/// </summary>
[LogIgnore]
public class DropListController : DomainForgeController
{
    private IDropListService<HTTPTokenProvider> _service = InjectionContext.Resolve<IDropListService<HTTPTokenProvider>>();

    /// <summary>
    /// 角色
    /// </summary>
    /// <returns></returns>
    [HttpGet, Route("Role")]
    public async Task<IApiResult<List<DTODropItem>>> Role() => (await _service.Role()).ToMLApiResult();

    /// <summary>
    /// 项目
    /// </summary>
    /// <returns></returns>
    [HttpGet, Route("Project")]
    public async Task<IApiResult<List<DTODropItem>>> Project() => (await _service.Project()).ToMLApiResult();

    /// <summary>
    /// 通道
    /// </summary>
    /// <returns></returns>
    [HttpGet, Route("Channel")]
    public async Task<IApiResult<DTOPageObj<DTOAppDSPChannelDropListResult>>> Channel(DTOAppDSPChannelDropList input) => (await _service.Channel(input)).ToMLApiResult();
}