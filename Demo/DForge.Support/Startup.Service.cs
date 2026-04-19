namespace DForge.Support;

public abstract class DomainForgeServiceStartup<TStartupSettings> : DomainForgeStartup<TStartupSettings>
    where TStartupSettings : DomainForgeStartupSettings
{
    public DomainForgeServiceStartup(IConfiguration configuration) : base(configuration) { }

    public void Configure(IApplicationBuilder app, IHostEnvironment env, IHostApplicationLifetime lifetime, ILoggerFactory loggerFactory)
    {
        var configures = new AppConfigure()
        {
            App = app,
            Env = env,
            Lifetime = lifetime,
            LoggerFactory = loggerFactory,
        };

        Configure(configures);
    }

    protected override void Extend_ConfigureServices(IServiceCollection services)
    {
        //
    }

    protected override void Extend_ConfigureContainer(ContainerBuilder containerBuilder)
    {
        containerBuilder.UseSnowID(8);
        containerBuilder.RegisSeriLogger(Configuration);
        containerBuilder.RegisAES($"{AppInitHelper.Environment}_{GlobalSettings.SystemName}_");
        containerBuilder.RegisAuth<FRedis>();
        containerBuilder.RegisRedis<FRedis>();
        containerBuilder.RegisRedis<FRedis, RedisSettings>();
        containerBuilder.RegisRedis<FRedis, MultilangSettings>();
        containerBuilder.RegisRedis<FRedis, LogicSupportSettings>();

        containerBuilder.CoverSettings<RenewRabbitMQSettings, RabbitMQSettings>(CurConfig.RabbitMQSettings);
        containerBuilder.RegisRabbitMQ();

        containerBuilder.MultilangFromFile(LanguageEnum.EN.ToString());

        RegisEF(containerBuilder);
    }

    protected override void Extend_Configure(AppConfigure configures)
    {
        //
    }
}