namespace YogoPop.Support;

public interface IRequestFilterHandle : ITransient
{
    public IRequestFilterItems FilterItems { get; }
}