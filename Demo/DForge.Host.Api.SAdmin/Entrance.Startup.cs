namespace DForge.Host.Api.SAdmin;

public class Startup : DomainForgeApiStartup<ApiSAdminSettings>
{
    public Startup(IConfiguration configuration) : base(configuration) { }

    protected override void Extend_ConfigureServices(IServiceCollection services)
    {
        base.Extend_ConfigureServices(services);
    }

    protected override void Extend_ConfigureContainer(ContainerBuilder containerBuilder)
    {
        base.Extend_ConfigureContainer(containerBuilder);

        containerBuilder.RegisterType<SAdminPermissionInitialization>().As<IPermissionInitialization>().SingleInstance();
    }

    protected override void Extend_Configure(AppConfigure configures)
    {
        base.Extend_Configure(configures);

        configures.Lifetime.RegisPermission();
        configures.Lifetime.ApplicationStarted.Register(() =>
        {
            InjectionContext.Resolve<MenuInitialization>().Run();
        });
    }
}