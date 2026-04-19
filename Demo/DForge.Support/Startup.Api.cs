namespace DForge.Support;

public abstract class DomainForgeApiStartup<TStartupSettings> : DomainForgeStartup<TStartupSettings> where TStartupSettings : DomainForgeStartupSettings
{
    public DomainForgeApiStartup(IConfiguration configuration) : base(configuration) { }

    public void Configure(IApplicationBuilder app, IHostEnvironment env, IHostApplicationLifetime lifetime, ILoggerFactory loggerFactory, IApiVersionDescriptionProvider apiVerDescProvider)
    {
        var configures = new AppConfigure()
        {
            App = app,
            Env = env,
            Lifetime = lifetime,
            LoggerFactory = loggerFactory,
            ApiVerDescProvider = apiVerDescProvider,
        };

        Configure(configures);
    }

    protected override void Extend_ConfigureServices(IServiceCollection services)
    {
        services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
        services
            .Configure<ApiBehaviorOptions>(options =>
            {
                //options.SuppressModelStateInvalidFilter = true;
            })
            .AddCors()
            .AddRouting(options =>
            {
                options.LowercaseUrls = true;
            })
            .AddControllers(options =>
            {
                options.Filters.Add<CtrlerFilterAttribute>();
            })
            .AddControllersAsServices()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
                if (!(JsonSerializerSettings.ContractResolver is CamelCasePropertyNamesContractResolver))
                    options.JsonSerializerOptions.PropertyNamingPolicy = null;
            })
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.Formatting = JsonSerializerSettings.Formatting;
                options.SerializerSettings.DateFormatString = JsonSerializerSettings.DateFormatString;
                options.SerializerSettings.ContractResolver = JsonSerializerSettings.ContractResolver;
                options.SerializerSettings.Converters = JsonSerializerSettings.Converters;
            });

        services.AddSwagger(CurConfig.SwaggerSettings, docDescription: SwaggerDescription);
        services.AddAutoMapper();
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

        containerBuilder.RegisStateCodeValueConverter<RenewStateCode>();
        containerBuilder.RegisPermission<GlobalPermissionEnum>();

        containerBuilder.MultilangFromFile(LanguageEnum.EN.ToString());

        RegisEF(containerBuilder);
    }

    protected override void Extend_Configure(AppConfigure configures)
    {
        if (configures.Env.IsDevelopment())
        {
            configures.App.UseDeveloperExceptionPage();
        }

        configures.App.UseCors(options =>
        {
            options.AllowAnyHeader();
            options.AllowAnyMethod();
            options.SetIsOriginAllowed(o => true);
            options.AllowCredentials();
        });

        configures.UseSwagger(CurConfig.SwaggerSettings);

        configures.App.UseRouting();
        configures.App.UseAuthorization();
        configures.App.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }

    protected virtual string SwaggerDescription => string.Empty;
}