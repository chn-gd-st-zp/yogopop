namespace DForge.Database.EF;

[DIModeForService(DIModeEnum.Exclusive, typeof(ISysSettingsRepository))]
public partial class SysSettingsRepository : RenewEFDBRepository<TBSysSettings, string>, ISysSettingsRepository
{
    public List<TBSysSettings> List(DTOSysSettingsList input)
    {
        var query = DBContext.GetQueryable<TBSysSettings>();

        if (input.Type.IsNotNullOrDefault())
            query = query.Where(o => o.Type == input.Type);

        if (input.Status.IsNotNullOrDefault())
            query = query.Where(o => o.Status == input.Status);

        var result = base.ListByQueryable<TBSysSettings, DTOSort>(query, input);

        return result;
    }

    public Tuple<List<TBSysSettings>, int> Page(DTOSysSettingsPage input)
    {
        var query = DBContext.GetQueryable<TBSysSettings>();

        if (input.Type.IsNotNullOrDefault())
            query = query.Where(o => o.Type == input.Type);

        var result = base.PageByQueryable<TBSysSettings, DTOSort>(query, input);

        return result;
    }
}