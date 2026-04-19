namespace DForge.Contract.SAdmin;

public interface ISysSettingsService<TTokenProvider, TDBFSysSettings, TDTOSysSettingsResult> : ITransient
    where TTokenProvider : ITokenProvider
    where TDBFSysSettings : DBFSysSettings
    where TDTOSysSettingsResult : DTOSysSettingsResult
{
    public Task<IServiceResult<bool>> Create(DTOSysSettingsCreate input);

    public Task<IServiceResult<bool>> Delete(DTOPrimaryKeyRequired<string> input);

    public Task<IServiceResult<bool>> Update(DTOSysSettingsUpdate input);

    public Task<IServiceResult<bool>> Edit(DTOSysSettingsEdit input);

    public Task<IServiceResult<bool>> Status(DTOSysSettingsStatus input);

    public Task<IServiceResult<TDTOSysSettingsResult>> Single(DTOSysSettingsSingle input);

    public Task<IServiceResult<TDTOSysSettingsResult>> Single(DTOPrimaryKeyRequired<string> input);

    public Task<IServiceResult<List<TDTOSysSettingsResult>>> List(DTOSysSettingsList input);
}