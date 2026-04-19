namespace YogoPop.Support;

[DIModeForService(DIModeEnum.ExclusiveByKeyed, typeof(IRequestFilterHandle), FilterTypeEnum.Ctrler)]
public class CtrlerFilterHandle : IRequestFilterHandle
{
    public IRequestFilterItems FilterItems { get { return _filterItems; } }
    private IRequestFilterItems _filterItems;

    public CtrlerFilterHandle()
    {
        _filterItems = InjectionContext.ResolveByKeyed<IRequestFilterItems>(FilterTypeEnum.Ctrler);
    }
}