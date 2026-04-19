namespace DForge.Host.Api.SAdmin.Controllers;

/// <summary>
/// DSP通道
/// </summary>
public class AppDSPChannelController : DomainForgeController
{
    private IAppDSPChannelService<HTTPTokenProvider> _service = InjectionContext.ResolveByKeyed<IAppDSPChannelService<HTTPTokenProvider>>(GlobalPermissionEnum.SAdmin);

    /// <summary>
    /// 创建
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost, Route("Create")]
    public async Task<IApiResult<bool>> Create(DTOAppDSPChannelCreate input) => (await _service.Create(input)).ToMLApiResult();

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
    public async Task<IApiResult<bool>> Update(DTOAppDSPChannelUpdate input) => (await _service.Update(input)).ToMLApiResult();

    /// <summary>
    /// 状态变更
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost, Route("Status")]
    public async Task<IApiResult<bool>> Status(DTOAppDSPChannelStatus input) => (await _service.Status(input)).ToMLApiResult();

    /// <summary>
    /// 详情
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost, Route("Single")]
    public async Task<IApiResult<DTOAppDSPChannelSingleResult>> Single(DTOPrimaryKeyRequired<string> input) => (await _service.Single(input)).ToMLApiResult();

    /// <summary>
    /// 分页
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost, Route("Page")]
    public async Task<IApiResult<DTOPageObj<DTOAppDSPChannelPageResult>>> Page(DTOAppDSPChannelPage input) => (await _service.Page(input)).ToMLApiResult();

    /// <summary>
    /// 同步发起
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost, Route("SyncApply")]
    public async Task<IApiResult<bool>> SyncApply(DTOPrimaryKeyRequired<string> input) => (await _service.Create(input)).ToMLApiResult();

    /// <summary>
    /// 同步记录
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost, Route("SyncPage")]
    public async Task<IApiResult<DTOPageObj<DTOAppDSPChannelSyncPageResult>>> SyncPage(DTOAppDSPChannelSyncPage input) => (await _service.Page(input)).ToMLApiResult();
}