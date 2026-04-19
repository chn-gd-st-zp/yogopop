namespace YogoPop.Core;

public static class VEnumExtension
{
    public static IVEnumItem NewEnum(this VEnumFactory enumParams, string name)
    {
        return NewEnum(enumParams, name, ++enumParams.Counter);
    }

    public static IVEnumItem NewEnum(this VEnumFactory enumParams, string name, int value)
    {
        enumParams.Counter = value;

        var cusEnum = new VEnumItem();
        cusEnum.Name = name;
        cusEnum.Value = value;

        enumParams.Members.Add(cusEnum);

        if (!enumParams.Keys.ContainsKey(cusEnum.Name))
            enumParams.Keys.Add(cusEnum.Name, cusEnum);

        if (!enumParams.Values.ContainsKey(cusEnum.Value))
            enumParams.Values.Add(cusEnum.Value, cusEnum);

        return cusEnum;
    }

    public static IVEnumItem Restore<TVEnum>(this object value, bool fromName = true)
    where TVEnum : IVEnum, new()
    {
        IVEnumItem result = null;

        var VEnum = IVEnum.Restore<TVEnum>();
        VEnum = VEnum == null ? new TVEnum() : VEnum;
        foreach (var property in VEnum.GetType().GetProperties())
        {
            if (!property.PropertyType.IsExtendOf(typeof(IVEnumItem)))
                continue;

            var prop = property.GetValue(VEnum);
            if (prop == null)
                continue;

            var enumItem = prop as IVEnumItem;
            if (enumItem == null)
                continue;

            if (fromName)
            {
                if (enumItem.Name != value.ToString())
                    continue;
            }
            else
            {
                if (enumItem.Value != (int)value)
                    continue;
            }

            result = enumItem;

            break;
        }

        return result;
    }

    public static ContainerBuilder RegisVEnumNameConverter<TVEnum>(this ContainerBuilder containerBuilder)
        where TVEnum : IVEnum, new()
    {
        containerBuilder.RegisterGeneric(typeof(VEnumNameConverter<>)).As(typeof(IVEnumConverter<>)).InstancePerDependency();

        return containerBuilder;
    }

    public static ContainerBuilder RegisVEnumValueConverter<TVEnum>(this ContainerBuilder containerBuilder)
        where TVEnum : IVEnum, new()
    {
        containerBuilder.RegisterGeneric(typeof(VEnumValueConverter<>)).As(typeof(IVEnumConverter<>)).InstancePerDependency();

        return containerBuilder;
    }
}