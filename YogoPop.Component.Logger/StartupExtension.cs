namespace YogoPop.Component.Logger;

public static partial class StartupExtension
{
    public static IHostBuilder UseNLogger(this IHostBuilder hostBuilder)
    {
        return hostBuilder
            .ConfigureLogging((hostingContext, config) =>
            {
                config.ClearProviders();
                config.AddNLog("nlog.config");
            })
            .UseNLog();
    }

    public static ContainerBuilder RegisNLogger(this ContainerBuilder containerBuilder, IConfiguration configuration)
    {
        containerBuilder.RegisterType<NLogger>().As<IYogoLogger>().SingleInstance();
        containerBuilder.RegisterGeneric(typeof(NLogger<>)).As(typeof(IYogoLogger<>)).SingleInstance();
        //containerBuilder.RegisterType<NLogger>().As<INLogger>().SingleInstance();
        //containerBuilder.RegisterGeneric(typeof(NLogger<>)).As(typeof(INLogger<>)).SingleInstance();

        return containerBuilder;
    }

    public static IHostBuilder UseSeriLogger(this IHostBuilder hostBuilder)
    {
        return hostBuilder
            .ConfigureLogging((hostingContext, config) =>
            {
                config.ClearProviders();
            })
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                config.AddJsonFile($"serilog.json", true, false);
            })
            .UseSerilog();
    }

    public static ContainerBuilder RegisSeriLogger(this ContainerBuilder containerBuilder, IConfiguration configuration)
    {
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .Enrich.WithProperty("Timestamp", () => DateTimeExtension.NowOffset)
            .CreateLogger();

        containerBuilder.RegisterType<SeriLogger>().As<IYogoLogger>().SingleInstance();
        containerBuilder.RegisterGeneric(typeof(SeriLogger<>)).As(typeof(IYogoLogger<>)).SingleInstance();
        //containerBuilder.RegisterType<SeriLogger>().As<ISeriLogger>().SingleInstance();
        //containerBuilder.RegisterGeneric(typeof(SeriLogger<>)).As(typeof(ISeriLogger<>)).SingleInstance();

        return containerBuilder;
    }
}
