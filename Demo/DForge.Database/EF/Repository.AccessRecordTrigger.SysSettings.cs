namespace DForge.Database.EF;

[DIModeForService(DIModeEnum.ExclusiveByKeyed, typeof(IAccessRecordTriggerRepository), typeof(TBSysSettings))]
public partial class SysSettingsRepository
{
    public IAccessRecordTrigger GetTriggerObj(object pk)
    {
        var query = DBContext.GetQueryable<TBSysSettings>();

        query = query.Where(o => o.PrimaryKey == pk.ToString());

        var result = query.SingleOrDefault();

        return result;
    }
}