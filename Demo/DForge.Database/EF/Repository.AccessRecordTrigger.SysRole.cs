namespace DForge.Database.EF;

[DIModeForService(DIModeEnum.ExclusiveByKeyed, typeof(IAccessRecordTriggerRepository), typeof(TBSysRole))]
public partial class SysRoleRepository
{
    public IAccessRecordTrigger GetTriggerObj(object pk)
    {
        var query = DBContext.GetQueryable<TBSysRole>();

        query = query.Where(o => o.PrimaryKey == pk.ToString());

        var result = query.SingleOrDefault();

        return result;
    }
}