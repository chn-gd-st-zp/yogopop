namespace YogoPop.Support;

public abstract class StartupBase<TStartupSettings, TConfigures>
    where TStartupSettings : IStartupSettings
    where TConfigures : IConfigureCollection
{
    public readonly List<string> TypeIgnore = new List<string>();
    public readonly List<string> TypeRegis = new List<string>();

    public StartupBase(IConfiguration configuration)
    {
        Configuration = configuration;
        CurConfig = Configuration.GetSetting<TStartupSettings>();
        JsonSerializerSettings = SetJsonSerializerSettings();
    }

    /// <summary>
    /// 配置
    /// </summary>
    public IConfiguration Configuration { get; private set; }

    /// <summary>
    /// 配置
    /// </summary>
    public TStartupSettings CurConfig { get; private set; }

    /// <summary>
    /// JSON设置
    /// </summary>
    protected JsonSerializerSettings JsonSerializerSettings { get; private set; }

    /// <summary>
    /// 设置序列化参数
    /// </summary>
    /// <returns></returns>
    protected abstract JsonSerializerSettings SetJsonSerializerSettings();

    /// <summary>
    /// 配置服务
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    //public IServiceProvider ConfigureServices(IServiceCollection services)
    //{
    //    services.AddOptions();

    //    Extend_ConfigureServices(services);

    //    var containerBuilder = new ContainerBuilder();

    //    containerBuilder.Populate(services);

    //    ConfigureContainer(containerBuilder);

    //    return new AutofacServiceProvider(containerBuilder.Build());
    //}

    /// <summary>
    /// 配置服务
    /// </summary>
    /// <param name="services"></param>
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddOptions();

        Extend_ConfigureServices(services);
    }

    /// <summary>
    /// 配置容器
    /// </summary>
    /// <param name="containerBuilder"></param>
    public void ConfigureContainer(ContainerBuilder containerBuilder)
    {
        var runningType = AppInitHelper.GetAllType(CurConfig.InjectionSettings.Patterns, CurConfig.InjectionSettings.Dlls);

        containerBuilder.Register(runningType, Configuration, TypeIgnore, TypeRegis);
        containerBuilder.Register(runningType, TypeIgnore, TypeRegis);

        containerBuilder.Register(o => Configuration).As<IConfiguration>().SingleInstance();
        containerBuilder.Register(o => CurConfig).AsSelf().SingleInstance();
        containerBuilder.Register(o => CurConfig.InjectionSettings).AsSelf().SingleInstance();
        containerBuilder.Register(o => JsonSerializerSettings).AsSelf().SingleInstance();

        Extend_ConfigureContainer(containerBuilder);
    }

    /// <summary>
    /// 配置程序
    /// </summary>
    /// <param name="configures"></param>
    protected void Configure(TConfigures configures)
    {
        InjectionContext.SetRoot(configures.App.ApplicationServices);

        Printor.PrintText(CurConfig.ToJson());
        Printor.PrintLine();

        configures.App.UseMiddleware<EnableBufferingMiddleware>();

        Extend_Configure(configures);
    }

    /// <summary>
    /// 拓展 ConfigureServices 方法
    /// </summary>
    /// <param name="services"></param>
    protected abstract void Extend_ConfigureServices(IServiceCollection services);

    /// <summary>
    /// 拓展 ConfigureContainer
    /// </summary>
    /// <param name="containerBuilder"></param>
    protected abstract void Extend_ConfigureContainer(ContainerBuilder containerBuilder);

    /// <summary>
    /// 拓展 Configure
    /// 给Web继承的
    /// </summary>
    /// <param name="configures"></param>
    protected abstract void Extend_Configure(TConfigures configures);
}