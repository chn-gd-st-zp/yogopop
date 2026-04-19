namespace DForge.Infrastructure.DBSupport.Repository;

public partial interface ISysSettingsRepository : IDBRepository<TBSysSettings, string>, ITransient
{
    public List<TBSysSettings> List(DTOSysSettingsList input);

    public Tuple<List<TBSysSettings>, int> Page(DTOSysSettingsPage input);
}