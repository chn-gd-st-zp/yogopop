namespace YogoPop.Core;

public interface IVEnum : ISingleton
{
    public static TVEnum Restore<TVEnum>() where TVEnum : IVEnum
    {
        return InjectionContext.Resolve<TVEnum>();
    }
}

public interface IVEnumItem
{
    public string Name { get; set; }

    public int Value { get; set; }

    public string ToStr() { return Name; }

    public int ToInt() { return Value; }

    public string ToIntString() { return Value.ToString(); }
}

public abstract class VEnum : IVEnum
{
    protected readonly VEnumFactory Factory;

    public VEnum() { Factory = new VEnumFactory(); }
}

public class VEnumItem : IVEnumItem
{
    public string Name { get; set; }

    public int Value { get; set; }

    public VEnumItem() { }

    internal string ToStr() { return Name; }

    internal int ToInt() { return Value; }

    public string ToIntString() { return Value.ToString(); }
}