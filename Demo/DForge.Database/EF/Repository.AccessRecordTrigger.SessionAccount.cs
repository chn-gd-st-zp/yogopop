namespace DForge.Database.EF;

[DIModeForService(DIModeEnum.ExclusiveByKeyed, typeof(IAccessRecordTriggerRepository), typeof(TBSessionAccount))]
public partial class SessionRepository
{
    public IAccessRecordTrigger GetTriggerObj(object pk)
    {
        var query = DBContext.GetQueryable<TBSessionAccount>();

        query = query.Where(o => o.PrimaryKey == pk.ToString());

        var result = query.SingleOrDefault();

        return result;
    }
}