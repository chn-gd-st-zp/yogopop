namespace DForge.Database.EF;

[DIModeForService(DIModeEnum.ExclusiveByKeyed, typeof(IAccessRecordTriggerRepository), typeof(TBAppDomain))]
public partial class AppDomainRepository
{
    public IAccessRecordTrigger GetTriggerObj(object pk)
    {
        var query = DBContext.GetQueryable<TBAppDomain>();

        query = query.Where(o => o.PrimaryKey == pk.ToString());

        var result = query.SingleOrDefault();

        return result;
    }
}