namespace YogoPop.Support;

[DIModeForService(DIModeEnum.ExclusiveByKeyed, typeof(IRequestFilterItems), FilterTypeEnum.Ctrler)]
public class CtrlerFilterItems : List<IRequestFilterItem>, IRequestFilterItems
{
    public CtrlerFilterItems()
    {
        Add(new CtrlerFilterItem());
    }
}