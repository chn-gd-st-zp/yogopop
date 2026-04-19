namespace DForge.Host.Api.SAdmin;

/// <summary>
/// 系统设置
/// </summary>
public abstract class SysSettingsControllerBase<TDBFSysSettings, TDTOSysSettingsResult> : DomainForgeController
    where TDBFSysSettings : DBFSysSettings
    where TDTOSysSettingsResult : DTOSysSettingsResult
{
    protected abstract SysSettingsEnum SettingsType { get; }

    protected readonly ISysSettingsService<HTTPTokenProvider, TDBFSysSettings, TDTOSysSettingsResult> SysSettingsService;

    public SysSettingsControllerBase() { SysSettingsService = InjectionContext.ResolveByKeyed<ISysSettingsService<HTTPTokenProvider, TDBFSysSettings, TDTOSysSettingsResult>>(GlobalPermissionEnum.SAdmin, SettingsType); }
}


/// <summary>
/// 系统设置
/// </summary>
public abstract class SysSettingsControllerBase<TDBFSysSettings, TDTOSysSettingsEdit, TDTOSysSettingsResult> : SysSettingsControllerBase<TDBFSysSettings, TDTOSysSettingsResult>
    where TDBFSysSettings : DBFSysSettings
    where TDTOSysSettingsEdit : DTOSysSettingsEdit
    where TDTOSysSettingsResult : DTOSysSettingsResult
{
    /// <summary>
    /// 编辑
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost, Route("Edit")]
    public virtual async Task<IApiResult<bool>> Edit(TDTOSysSettingsEdit input) => (await SysSettingsService.Edit(input)).ToMLApiResult();

    /// <summary>
    /// 详情
    /// </summary>
    /// <returns></returns>
    [HttpGet, Route("Single")]
    public virtual async Task<IApiResult<TDTOSysSettingsResult>> Single() => (await SysSettingsService.Single(new DTOSysSettingsSingle { Type = SettingsType })).ToMLApiResult();
}


/// <summary>
/// 系统设置
/// </summary>
public abstract class SysSettingsControllerBase<TDBFSysSettings, TDTOSysSettingsCreate, TDTOSysSettingsUpdate, TDTOSysSettingsResult> : SysSettingsControllerBase<TDBFSysSettings, TDTOSysSettingsResult>
    where TDBFSysSettings : DBFSysSettings
    where TDTOSysSettingsCreate : DTOSysSettingsCreate
    where TDTOSysSettingsUpdate : DTOSysSettingsUpdate
    where TDTOSysSettingsResult : DTOSysSettingsResult
{
    /// <summary>
    /// 创建
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost, Route("Create")]
    public virtual async Task<IApiResult<bool>> Create(TDTOSysSettingsCreate input) => (await SysSettingsService.Create(input)).ToMLApiResult();

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpDelete, Route("Delete")]
    public virtual async Task<IApiResult<bool>> Delete(DTOPrimaryKeyRequired<string> input) => (await SysSettingsService.Delete(input)).ToMLApiResult();

    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost, Route("Update")]
    public virtual async Task<IApiResult<bool>> Update(TDTOSysSettingsUpdate input) => (await SysSettingsService.Update(input)).ToMLApiResult();

    /// <summary>
    /// 状态变更
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost, Route("Status")]
    public virtual async Task<IApiResult<bool>> Status(DTOSysSettingsStatus input) => (await SysSettingsService.Status(input)).ToMLApiResult();

    /// <summary>
    /// 详情
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost, Route("Single")]
    public virtual async Task<IApiResult<TDTOSysSettingsResult>> Single(DTOPrimaryKeyRequired<string> input) => (await SysSettingsService.Single(input)).ToMLApiResult();

    /// <summary>
    /// 列表
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost, Route("List")]
    public virtual async Task<IApiResult<List<TDTOSysSettingsResult>>> List(DTOSysSettingsList input)
    {
        input.Type = SettingsType;

        var result = await SysSettingsService.List(input);

        return result.ToMLApiResult();
    }
}