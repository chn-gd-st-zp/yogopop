namespace YogoPop.Component.Dispatcher;

public static class StartupExtension
{
    public static IServiceCollection RegisDispatcher(this IServiceCollection services, IStartupSettings startupSettings, DispatcherSettings settings)
    {
        if (settings.RunningServices.IsEmpty())
            return services;

        var hsType = typeof(IHostedService);

        AppInitHelper
            .GetAllType(startupSettings.InjectionSettings.Patterns, startupSettings.InjectionSettings.Dlls)
            .Where(o => o.IsClass && o.IsImplementedOf(hsType))
            .Select(o => o.GetCustomAttribute<DIModeForServiceAttribute>())
            .Where(o => o != null)
            .ToList()
            .ForEach(o =>
            {
                var dispatcherType = o.Key.ToString().ToEnum<DispatcherTypeEnum>();
                if (settings.RunningServices.Contains(dispatcherType))
                {
                    services.AddSingleton(hsType, o.Type);

                    switch (dispatcherType)
                    {
                        case DispatcherTypeEnum.HangFireTiming:
                            {
                                services.AddTransient<IHangFireTimingRegister, HangFireTimingRegister>();
                                services
                                    .AddHangfire(configuration =>
                                    {
                                        configuration.SetDataCompatibilityLevel(CompatibilityLevel.Version_170);
                                        configuration.UseSimpleAssemblyNameTypeSerializer();
                                        configuration.UseRecommendedSerializerSettings();
                                        configuration.UseSerilogLogProvider();
                                        configuration.UseMemoryStorage();
                                        configuration.UseFilter(new AutomaticRetryAttribute { Attempts = 0 });
                                    })
                                    .AddHangfireServer();
                            }
                            break;
                        case DispatcherTypeEnum.QuartzTiming:
                            {
                                services.AddTransient<IQuartzTimingRegister, QuartzTimingRegister>();

                                services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
                                services.AddSingleton<IJobFactory, JobFactory>();

                                AppInitHelper
                                    .GetAllType(startupSettings.InjectionSettings.Patterns, startupSettings.InjectionSettings.Dlls)
                                    .Where(o => o.IsClass && o.IsImplementedOf<IQuartzTimingDispatcher>())
                                    .ToList()
                                    .ForEach(o =>
                                    {
                                        var attr = o.GetCustomAttribute<JobTypeAttribute>();
                                        if (attr != null && attr.Type.IsImplementedOf<IJob>() && attr.Type.IsImplementedOf<IQuartzTimingDispatcher>())
                                            services.AddTransient(attr.Type, o);
                                    });
                            }
                            break;
                    }
                }
            });

        return services;
    }
}