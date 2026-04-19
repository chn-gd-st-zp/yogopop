namespace YogoPop.Component.Permission;

public static partial class StartupExtansion
{
    public static ContainerBuilder RegisPermission<TPermissionEnum>(this ContainerBuilder containerBuilder)
    {
        containerBuilder.Register(o => new PermissionEnum(typeof(TPermissionEnum))).As<IPermissionEnum>().SingleInstance();

        return containerBuilder;
    }

    public static IHostApplicationLifetime RegisPermission(this IHostApplicationLifetime lifetime)
    {
        var lifeTime = new PermissionLifeTime();

        lifetime.ApplicationStarted.Register(() => lifeTime.Started());
        lifetime.ApplicationStopping.Register(() => lifeTime.Stopping());
        lifetime.ApplicationStopped.Register(() => lifeTime.Stopped());

        return lifetime;
    }
}