namespace YogoPop.DB.Define;

[AttributeUsage(AttributeTargets.Property)]
public class SortAttribute : Attribute
{
    public SortAttribute(string realName, params string[] nickNames)
    {
        RealName = realName;
        NickNames = nickNames;
    }

    public string RealName { get; private set; }

    public string[] NickNames { get; private set; }
}