namespace YogoPop.Core;

internal interface IVEnumConverter<TVEnum> : ISingleton
    where TVEnum : IVEnum, new()
{
    IVEnumItem Read(object value);

    object Write(IVEnumItem enumItem);
}

internal class VEnumNameConverter<TVEnum> : IVEnumConverter<TVEnum>
    where TVEnum : IVEnum, new()
{
    public IVEnumItem Read(object value)
    {
        return value.Restore<TVEnum>();
    }

    public object Write(IVEnumItem enumItem)
    {
        if (enumItem == null) return null;
        return enumItem.Name;
    }
}

internal class VEnumValueConverter<TVEnum> : IVEnumConverter<TVEnum>
    where TVEnum : IVEnum, new()
{
    public IVEnumItem Read(object value)
    {
        return value.Restore<TVEnum>(false);
    }

    public object Write(IVEnumItem enumItem)
    {
        if (enumItem == null) return null;
        return enumItem.Value;
    }
}