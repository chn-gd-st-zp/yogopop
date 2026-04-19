namespace YogoPop.Core.StateCode;

public static class StateCodeExtension
{
    public static ContainerBuilder RegisStateCodeNameConverter<TStateCode>(this ContainerBuilder containerBuilder) where TStateCode : IStateCode, new()
    {
        return containerBuilder.RegisVEnumNameConverter<TStateCode>();
    }

    public static ContainerBuilder RegisStateCodeValueConverter<TStateCode>(this ContainerBuilder containerBuilder) where TStateCode : IStateCode, new()
    {
        return containerBuilder.RegisVEnumValueConverter<TStateCode>();
    }
}