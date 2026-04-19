namespace YogoPop.Component.Auth;

public static partial class StartupExtansion
{
    public static ContainerBuilder RegisAuth<TRedis>(this ContainerBuilder containerBuilder) where TRedis : ICache4Redis
    {
        containerBuilder.RegisRedis<TRedis, AuthSettings>();
        containerBuilder.RegisterType<YogoSessionContext>().Keyed<IYogoSessionContext>(ProtocolEnum.HTTP).InstancePerDependency();
        containerBuilder.RegisterType<YogoSessionContext>().Keyed<IYogoSessionContext>(ProtocolEnum.HTTP.ToString()).InstancePerDependency();
        containerBuilder.RegisterType<YogoSessionContext>().Keyed<IYogoSessionContext>((int)ProtocolEnum.HTTP).InstancePerDependency();
        containerBuilder.RegisterGeneric(typeof(YogoSession<>)).As(typeof(IYogoSession<>)).InstancePerDependency();
        return containerBuilder;
    }
}