namespace YogoPop.DB.Define;

[AttributeUsage(AttributeTargets.Class)]
public class DefaultSortAttribute : Attribute
{
    public DefaultSortAttribute(string realName, SortDirectionEnum direction)
    {
        RealName = realName;
        Direction = direction;
    }

    public string RealName { get; private set; }

    public SortDirectionEnum Direction { get; private set; }
}