namespace DForge.Database.EF;

[DIModeForService(DIModeEnum.ExclusiveByKeyed, typeof(IAccessRecordTriggerRepository), typeof(TBAppDNSRecord))]
public partial class AppDNSRecordRepository
{
    public IAccessRecordTrigger GetTriggerObj(object pk)
    {
        var query = DBContext.GetQueryable<TBAppDNSRecord>();

        query = query.Where(o => o.PrimaryKey == pk.ToString());

        var result = query.SingleOrDefault();

        return result;
    }
}