namespace YogoPop.Component.VerificationCode;

public static partial class StartupExtansion
{
    public static ContainerBuilder RegisVCHandler(this ContainerBuilder containerBuilder, InjectionSettings injectionSettings)
    {
        AppInitHelper.GetAllType(injectionSettings.Patterns, injectionSettings.Dlls)
            .Where(o => o.IsImplementedOf<IHandler>() && !o.IsAbstract)
            .ToList()
            .ForEach(o =>
            {
                var obj = InstanceCreator.Create(o) as IHandler;
                containerBuilder.Register(oo => obj).Named<IHandler>(obj.Provider).InstancePerLifetimeScope();
            });

        return containerBuilder;
    }
}