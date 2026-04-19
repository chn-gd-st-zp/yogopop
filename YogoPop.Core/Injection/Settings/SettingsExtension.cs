namespace YogoPop.Core.Injection.Settings;

public static class SettingsExtension
{
    /// <summary>
    /// 覆盖注入配置
    /// </summary>
    /// <typeparam name="TNewSettings"></typeparam>
    /// <typeparam name="TDistSettings"></typeparam>
    /// <param name="containerBuilder"></param>
    /// <param name="newSettings"></param>
    /// <returns></returns>
    public static ContainerBuilder CoverSettings<TNewSettings, TDistSettings>(this ContainerBuilder containerBuilder, TNewSettings newSettings)
        where TNewSettings : class, TDistSettings
        where TDistSettings : class, ISettings
    {
        containerBuilder.Register(o => newSettings).As(typeof(TDistSettings)).SingleInstance();

        return containerBuilder;
    }

    /// <summary>
    /// 监控配置
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    public static void Monitor<T>(this IServiceCollection services, IConfiguration configuration) where T : class, ISettings
    {
        services.Configure<T>(configuration);
    }

    /// <summary>
    /// 监听配置更新
    /// </summary>
    /// <param name="app"></param>
    /// <param name="typeList"></param>
    public static void MonitorSettings(this IApplicationBuilder app, IEnumerable<Type> typeList)
    {
        foreach (var classType in typeList.Where(o => o.IsClass && !o.IsAbstract).ToList())
        {
            try
            {
                if (!classType.IsImplementedOf<ISettings>())
                    continue;

                typeof(SettingsExtension).GetMethod(nameof(SettingsOnChange))
                    .MakeGenericMethod(new[] { classType })
                    .Invoke(null, null);
            }
            catch
            {
                continue;
            }
        }
    }

    /// <summary>
    /// 动态更新配置
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static void SettingsOnChange<T>() where T : class
    {
        var objType = typeof(T);
        if (!objType.IsImplementedOf<ISettings>())
            return;

        var optionsMonitor = InjectionContext.Resolve<IOptionsMonitor<T>>();
        optionsMonitor.OnChange((T, listener) =>
        {
            var attrList = objType.GetCustomAttributes<DIModeForSettingsAttribute>();
            if (attrList.IsEmpty())
                return;

            var piArray = objType.GetProperties();
            var configuration = InjectionContext.Resolve<IConfiguration>();

            T obj_target = null;
            T obj_source = null;

            foreach (var attr in attrList)
            {
                obj_source = configuration.GetSetting(attr.ConfigRootName, objType) as T;

                switch (attr.DIMode)
                {
                    case DIModeEnum.AsSelf:
                        obj_target = InjectionContext.Resolve<T>();
                        break;
                    case DIModeEnum.AsImpl:

                        Type interfaceType = null;
                        foreach (var interfaceItem in objType.GetInterfaces())
                        {
                            if (interfaceItem is ISettings || interfaceItem.IsImplementedOf<ISettings>())
                            {
                                interfaceType = interfaceItem;
                                break;
                            }
                        }

                        if (interfaceType == null)
                            continue;

                        obj_target = InjectionContext.Resolve(interfaceType) as T;
                        break;
                    case DIModeEnum.Exclusive:
                        obj_target = InjectionContext.Resolve(attr.Type) as T;
                        break;
                    case DIModeEnum.ExclusiveByNamed:
                        obj_target = InjectionContext.ResolveByNamed(attr.Type, attr.Key.ToString()) as T;
                        break;
                    case DIModeEnum.ExclusiveByKeyed:
                        obj_target = InjectionContext.ResolveByKeyed(attr.Type, attr.Key.ToString()) as T;
                        break;
                }

                if (obj_target == null || obj_source == null)
                    continue;

                foreach (var pi in piArray)
                    pi.SetValue(obj_target, pi.GetValue(obj_source));
            }
        });
    }
}