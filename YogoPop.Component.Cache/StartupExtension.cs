namespace YogoPop.Component.Cache;

public static partial class StartupExtension
{
    public static ContainerBuilder RegisRedis<TRedis>(this ContainerBuilder containerBuilder) where TRedis : ICache4Redis
    {
        containerBuilder.RegisterType<TRedis>().As<TRedis>().InstancePerDependency();
        containerBuilder.RegisterType<TRedis>().As<ICache>().InstancePerDependency();
        containerBuilder.RegisterType<TRedis>().As<ICache4Redis>().InstancePerDependency();

        return containerBuilder;
    }

    public static ContainerBuilder RegisRedis<TRedis, TKeyed>(this ContainerBuilder containerBuilder) where TRedis : ICache4Redis
    {
        containerBuilder.RegisterType<TRedis>().Keyed<TRedis>(typeof(TKeyed)).InstancePerDependency();
        containerBuilder.RegisterType<TRedis>().Keyed<ICache>(typeof(TKeyed)).InstancePerDependency();
        containerBuilder.RegisterType<TRedis>().Keyed<ICache4Redis>(typeof(TKeyed)).InstancePerDependency();

        return containerBuilder;
    }

    public static TCache ResolveCache<TCache>(this ICacheSettings settings, bool fullyCover = false) where TCache : ICache
    {
        if (settings == null) return default;

        using var diScope = InjectionContext.Root.CreateScope();
        return ResolveCache<TCache>(diScope, settings, fullyCover);
    }

    public static TCache ResolveCache<TCache>(this IServiceScope diScope, ICacheSettings settings, bool fullyCover = false) where TCache : ICache
    {
        if (settings == null) return default;

        var redisSettings_source = settings.ToJson().ToObject<RedisSettings>();
        var redisSettings_using = diScope.Resolve<RedisSettings>().DeepCopy();

        redisSettings_using.DBIndex = redisSettings_source.DBIndex;
        redisSettings_using.Prefix = fullyCover ? redisSettings_source.Prefix : redisSettings_using.Prefix + redisSettings_source.Prefix;

        //return diScope.ResolveByKeyed<TCache>(settings.GetType(), new NamedPropertyParameter("redisSettings", redisSettings), new NamedPropertyParameter("defaultDatabase", redisSettings.DBIndex));
        //return diScope.ResolveByKeyed<TCache>(settings.GetType(), redisSettings.ToNamedPropertyParameter("redisSettings"), redisSettings.DBIndex.ToNamedPropertyParameter("defaultDatabase"));

        //return diScope.ResolveByKeyed<TCache>(settings.GetType(),  new TypedParameter(typeof(RedisSettings), redisSettings), new TypedParameter(typeof(int), redisSettings.DBIndex));
        return diScope.ResolveByKeyed<TCache>(settings.GetType(), redisSettings_using.ToTypedParameter<RedisSettings>(), redisSettings_using.DBIndex.ToTypedParameter<int>());
    }
}