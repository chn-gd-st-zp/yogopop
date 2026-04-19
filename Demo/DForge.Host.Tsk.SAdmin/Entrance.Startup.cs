namespace DForge.Host.SDispatcher;

public class Startup : DomainForgeServiceStartup<TskSAdminSettings>
{
    public Startup(IConfiguration configuration) : base(configuration) { }

    protected override void Extend_ConfigureServices(IServiceCollection services)
    {
        base.Extend_ConfigureServices(services);

        services.RegisDispatcher(CurConfig, CurConfig.DispatcherSettings);
    }

    protected override void Extend_ConfigureContainer(ContainerBuilder containerBuilder)
    {
        base.Extend_ConfigureContainer(containerBuilder);
    }

    protected override void Extend_Configure(AppConfigure configures)
    {
        base.Extend_Configure(configures);

        EnumExtension.ToEnumList<DynSchEnum>()
            .ForEach(async enumType =>
            {
                var attrs = enumType.GetAttributes<DynSchPeriodAttribute>();
                if (attrs.IsNotEmpty())
                {
                    attrs.ForEach(async attr =>
                    {
                        var runner = InjectionContext.ResolveByNamed<IHangFireTimingDispatcher>(nameof(RDynSchCreator));
                        InjectionContext.Resolve<IHangFireTimingRegister>().Regis(runner, attr.Cron, new TimingParams
                        {
                            Name = $"{nameof(RDynSchCreator)}-{enumType}-{attr.DateCycle}",
                            Args = new string[] { enumType.ToString(), attr.DateCycle.ToString() }
                        });
                    });
                }
            });

        configures.App.UseHangfireDashboard("/hangfire", new DashboardOptions()
        {
            Authorization = new[] { new CustomAuthorizeFilter() }
        });
    }
}