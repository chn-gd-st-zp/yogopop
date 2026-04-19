namespace YogoPop.Core.Injection;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
public class DIModeAttribute : Attribute
{
    public DIModeEnum DIMode { get; set; }

    public Type Type { get; set; }
}

[AttributeUsage(AttributeTargets.Class)]
public class DIModeForArrayItemAttribute : Attribute { }

public class DIModeForServiceAttribute : DIModeAttribute
{
    public object Key { get; private set; }

    public List<Parameter> Params { get; private set; }

    public DIModeForServiceAttribute()
    {
        DIMode = DIModeEnum.AsSelf;
    }

    public DIModeForServiceAttribute(DIModeEnum diMode, Type type = null)
    {
        DIMode = diMode;
        Type = type;
    }

    public DIModeForServiceAttribute(DIModeEnum diMode, Type type, object key)
    {
        DIMode = diMode;
        Type = type;
        Key = key;
    }

    public DIModeForServiceAttribute(DIModeEnum diMode, Type type, object key, params Parameter[] paramArray)
    {
        DIMode = diMode;
        Type = type;
        Key = key;
        Params = paramArray.ToList();
    }

    public DIModeForServiceAttribute(DIModeEnum diMode, Type type, params object[] keys)
    {
        DIMode = diMode;
        Type = type;
        Key = InjectionExtension.CombineKeyeds(keys);
    }
}

public class DIModeForSettingsAttribute : DIModeAttribute
{
    public string ConfigRootName { get; private set; }

    public DIKeyedNamedFromEnum KNFrom { get; private set; } = DIKeyedNamedFromEnum.None;

    public object Key { get; private set; }

    public DIModeForSettingsAttribute(string configRootName, Type type = null)
    {
        ConfigRootName = configRootName;
        Type = type;
        DIMode = DIModeEnum.Exclusive;
    }

    public DIModeForSettingsAttribute(string configRootName, Type type = null, DIKeyedNamedFromEnum knFrom = DIKeyedNamedFromEnum.FromProperty, object key = null)
    {
        ConfigRootName = configRootName;
        Type = type;
        DIMode = DIModeEnum.ExclusiveByKeyed;
        Key = key;
        KNFrom = knFrom;
    }
}