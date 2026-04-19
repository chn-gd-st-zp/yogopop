namespace DForge.Database.EF;

[DIModeForService(DIModeEnum.ExclusiveByKeyed, typeof(IAccessRecordTriggerRepository), typeof(TBAppDynSchRecord))]
public partial class AppDynSchRecordRepository
{
    public IAccessRecordTrigger GetTriggerObj(object pk)
    {
        var query = DBContext.GetQueryable<TBAppDynSchRecord>();

        query = query.Where(o => o.PrimaryKey == long.Parse(pk.ToString()));

        var result = query.SingleOrDefault();

        return result;
    }
}