namespace YogoPop.Core.Injection;

public class InjectionContext
{
    private static IServiceProvider _root = null;
    public static IServiceProvider Root => _root;
    public static void SetRoot(IServiceProvider root) => _root = root;
    public static bool IsDoneLoad => _root != null;


    public static IServiceProvider GetProvider(Type type)
    {
        if (type.IsImplementedOf(typeof(ISingleton))) return _root;

        var hca = ServiceProviderServiceExtensions.GetService<IHttpContextAccessor>(_root);
        return hca?.HttpContext?.RequestServices != null ? hca.HttpContext.RequestServices : _root;
    }


    public static object Resolve(Type type) => GetProvider(type).Resolve(type);

    public static object Resolve(Type type, params Parameter[] parameters) => GetProvider(type).Resolve(type, parameters);

    public static object Resolve(Type type, IEnumerable<Parameter> parameters) => GetProvider(type).Resolve(type, parameters);

    public static T Resolve<T>() => GetProvider(typeof(T)).Resolve<T>();

    public static T Resolve<T>(params Parameter[] parameters) => GetProvider(typeof(T)).Resolve<T>(parameters);

    public static T Resolve<T>(IEnumerable<Parameter> parameters) => GetProvider(typeof(T)).Resolve<T>(parameters);


    public static object ResolveByNamed(Type type, string named) => GetProvider(type).ResolveByNamed(type, named);

    public static object ResolveByNamed(Type type, string named, params Parameter[] parameters) => GetProvider(type).ResolveByNamed(type, named, parameters);

    public static object ResolveByNamed(Type type, string named, IEnumerable<Parameter> parameters) => GetProvider(type).ResolveByNamed(type, named, parameters);

    public static T ResolveByNamed<T>(string named) => GetProvider(typeof(T)).ResolveByNamed<T>(named);

    public static T ResolveByNamed<T>(string named, params Parameter[] parameters) => GetProvider(typeof(T)).ResolveByNamed<T>(named, parameters);

    public static T ResolveByNamed<T>(string named, IEnumerable<Parameter> parameters) => GetProvider(typeof(T)).ResolveByNamed<T>(named, parameters);

    public static object ResolveByNamed(Type type, params string[] nameds) => GetProvider(type).ResolveByNamed(type, InjectionExtension.CombineKeyeds(nameds));

    public static T ResolveByNamed<T>(params string[] nameds) => GetProvider(typeof(T)).ResolveByNamed<T>(InjectionExtension.CombineKeyeds(nameds));

    public static T ResolveByNamed<T>(string[] nameds, params Parameter[] parameters) => GetProvider(typeof(T)).ResolveByNamed<T>(InjectionExtension.CombineKeyeds(nameds), parameters);

    public static T ResolveByNamed<T>(string[] nameds, IEnumerable<Parameter> parameters) => GetProvider(typeof(T)).ResolveByNamed<T>(InjectionExtension.CombineKeyeds(nameds), parameters);


    public static object ResolveByKeyed(Type type, object keyed) => GetProvider(type).ResolveByKeyed(type, keyed);

    public static object ResolveByKeyed(Type type, object keyed, params Parameter[] parameters) => GetProvider(type).ResolveByKeyed(type, keyed, parameters);

    public static object ResolveByKeyed(Type type, object keyed, IEnumerable<Parameter> parameters) => GetProvider(type).ResolveByKeyed(type, keyed, parameters);

    public static T ResolveByKeyed<T>(object keyed) => GetProvider(typeof(T)).ResolveByKeyed<T>(keyed);

    public static T ResolveByKeyed<T>(object keyed, params Parameter[] parameters) => GetProvider(typeof(T)).ResolveByKeyed<T>(keyed, parameters);

    public static T ResolveByKeyed<T>(object keyed, IEnumerable<Parameter> parameters) => GetProvider(typeof(T)).ResolveByKeyed<T>(keyed, parameters);

    public static object ResolveByKeyed(Type type, params object[] keyeds) => GetProvider(type).ResolveByKeyed(type, InjectionExtension.CombineKeyeds(keyeds));

    public static T ResolveByKeyed<T>(params object[] keyeds) => GetProvider(typeof(T)).ResolveByKeyed<T>(InjectionExtension.CombineKeyeds(keyeds));

    public static T ResolveByKeyed<T>(object[] keyeds, params Parameter[] parameters) => GetProvider(typeof(T)).ResolveByKeyed<T>(InjectionExtension.CombineKeyeds(keyeds), parameters);

    public static T ResolveByKeyed<T>(object[] keyeds, IEnumerable<Parameter> parameters) => GetProvider(typeof(T)).ResolveByKeyed<T>(InjectionExtension.CombineKeyeds(keyeds), parameters);
}

public static class IServiceScopeExtend
{
    public static IServiceProvider GetProvider(this IServiceScope serviceScope, Type type) => type.IsImplementedOf(typeof(ISingleton)) ? InjectionContext.Root : serviceScope.ServiceProvider;


    public static object Resolve(this IServiceScope serviceScope, Type type) => serviceScope.GetProvider(type).Resolve(type);

    public static object Resolve(this IServiceScope serviceScope, Type type, params Parameter[] parameters) => serviceScope.GetProvider(type).Resolve(type, parameters);

    public static object Resolve(this IServiceScope serviceScope, Type type, IEnumerable<Parameter> parameters) => serviceScope.GetProvider(type).Resolve(type, parameters);

    public static T Resolve<T>(this IServiceScope serviceScope) => serviceScope.GetProvider(typeof(T)).Resolve<T>();

    public static T Resolve<T>(this IServiceScope serviceScope, params Parameter[] parameters) => serviceScope.GetProvider(typeof(T)).Resolve<T>(parameters);

    public static T Resolve<T>(this IServiceScope serviceScope, IEnumerable<Parameter> parameters) => serviceScope.GetProvider(typeof(T)).Resolve<T>(parameters);


    public static object ResolveByNamed(this IServiceScope serviceScope, Type type, string named) => serviceScope.GetProvider(type).ResolveByNamed(type, named);

    public static object ResolveByNamed(this IServiceScope serviceScope, Type type, string named, params Parameter[] parameters) => serviceScope.GetProvider(type).ResolveByNamed(type, named, parameters);

    public static object ResolveByNamed(this IServiceScope serviceScope, Type type, string named, IEnumerable<Parameter> parameters) => serviceScope.GetProvider(type).ResolveByNamed(type, named, parameters);

    public static T ResolveByNamed<T>(this IServiceScope serviceScope, string named) => serviceScope.GetProvider(typeof(T)).ResolveByNamed<T>(named);

    public static T ResolveByNamed<T>(this IServiceScope serviceScope, string named, params Parameter[] parameters) => serviceScope.GetProvider(typeof(T)).ResolveByNamed<T>(named, parameters);

    public static T ResolveByNamed<T>(this IServiceScope serviceScope, string named, IEnumerable<Parameter> parameters) => serviceScope.GetProvider(typeof(T)).ResolveByNamed<T>(named, parameters);

    public static object ResolveByNamed(this IServiceScope serviceScope, Type type, params string[] nameds) => serviceScope.GetProvider(type).ResolveByNamed(type, InjectionExtension.CombineKeyeds(nameds));

    public static T ResolveByNamed<T>(this IServiceScope serviceScope, params string[] nameds) => serviceScope.GetProvider(typeof(T)).ResolveByNamed<T>(InjectionExtension.CombineKeyeds(nameds));

    public static T ResolveByNamed<T>(this IServiceScope serviceScope, string[] nameds, params Parameter[] parameters) => serviceScope.GetProvider(typeof(T)).ResolveByNamed<T>(InjectionExtension.CombineKeyeds(nameds), parameters);

    public static T ResolveByNamed<T>(this IServiceScope serviceScope, string[] nameds, IEnumerable<Parameter> parameters) => serviceScope.GetProvider(typeof(T)).ResolveByNamed<T>(InjectionExtension.CombineKeyeds(nameds), parameters);


    public static object ResolveByKeyed(this IServiceScope serviceScope, Type type, object keyed) => serviceScope.GetProvider(type).ResolveByKeyed(type, keyed);

    public static object ResolveByKeyed(this IServiceScope serviceScope, Type type, object keyed, params Parameter[] parameters) => serviceScope.GetProvider(type).ResolveByKeyed(type, keyed, parameters);

    public static object ResolveByKeyed(this IServiceScope serviceScope, Type type, object keyed, IEnumerable<Parameter> parameters) => serviceScope.GetProvider(type).ResolveByKeyed(type, keyed, parameters);

    public static T ResolveByKeyed<T>(this IServiceScope serviceScope, object keyed) => serviceScope.GetProvider(typeof(T)).ResolveByKeyed<T>(keyed);

    public static T ResolveByKeyed<T>(this IServiceScope serviceScope, object keyed, params Parameter[] parameters) => serviceScope.GetProvider(typeof(T)).ResolveByKeyed<T>(keyed, parameters);

    public static T ResolveByKeyed<T>(this IServiceScope serviceScope, object keyed, IEnumerable<Parameter> parameters) => serviceScope.GetProvider(typeof(T)).ResolveByKeyed<T>(keyed, parameters);

    public static object ResolveByKeyed(this IServiceScope serviceScope, Type type, params object[] keyeds) => serviceScope.GetProvider(type).ResolveByKeyed(type, InjectionExtension.CombineKeyeds(keyeds));

    public static T ResolveByKeyed<T>(this IServiceScope serviceScope, params object[] keyeds) => serviceScope.GetProvider(typeof(T)).ResolveByKeyed<T>(InjectionExtension.CombineKeyeds(keyeds));

    public static T ResolveByKeyed<T>(this IServiceScope serviceScope, object[] keyeds, params Parameter[] parameters) => serviceScope.GetProvider(typeof(T)).ResolveByKeyed<T>(InjectionExtension.CombineKeyeds(keyeds), parameters);

    public static T ResolveByKeyed<T>(this IServiceScope serviceScope, object[] keyeds, IEnumerable<Parameter> parameters) => serviceScope.GetProvider(typeof(T)).ResolveByKeyed<T>(InjectionExtension.CombineKeyeds(keyeds), parameters);
}

public static class IServiceProviderExtend
{
    public static object GetService(this IServiceProvider serviceProvider, Type type)
    {
        try
        {
            if (serviceProvider == null)
                return default;

            var obj = serviceProvider.GetService(type.GetType());
            if (obj == null)
                return default;

            return obj;
        }
        catch
        {
            return default;
        }
    }

    public static T GetService<T>(this IServiceProvider serviceProvider)
    {
        try
        {
            if (serviceProvider == null)
                return default;

            var obj = serviceProvider.GetService(typeof(T));
            if (obj == null)
                return default;

            return (T)obj;
        }
        catch
        {
            return default;
        }
    }


    public static object Resolve(this IServiceProvider serviceProvider, Type type)
    {
        try
        {
            var context = serviceProvider.GetService<IComponentContext>();
            if (context == null)
                return default;

            return context.Resolve(type);
        }
        catch
        {
            return default;
        }
    }

    public static object Resolve(this IServiceProvider serviceProvider, Type type, params Parameter[] parameters)
    {
        try
        {
            var context = serviceProvider.GetService<IComponentContext>();
            if (context == null)
                return default;

            return context.Resolve(type, parameters);
        }
        catch
        {
            return default;
        }
    }

    public static object Resolve(this IServiceProvider serviceProvider, Type type, IEnumerable<Parameter> parameters)
    {
        try
        {
            var context = serviceProvider.GetService<IComponentContext>();
            if (context == null)
                return default;

            return context.Resolve(type, parameters);
        }
        catch
        {
            return default;
        }
    }

    public static T Resolve<T>(this IServiceProvider serviceProvider)
    {
        try
        {
            var context = serviceProvider.GetService<IComponentContext>();
            if (context == null)
                return default;

            return context.Resolve<T>();
        }
        catch
        {
            return default;
        }
    }

    public static T Resolve<T>(this IServiceProvider serviceProvider, params Parameter[] parameters)
    {
        try
        {
            var context = serviceProvider.GetService<IComponentContext>();
            if (context == null)
                return default;

            return context.Resolve<T>(parameters);
        }
        catch
        {
            return default;
        }
    }

    public static T Resolve<T>(this IServiceProvider serviceProvider, IEnumerable<Parameter> parameters)
    {
        try
        {
            var context = serviceProvider.GetService<IComponentContext>();
            if (context == null)
                return default;

            return context.Resolve<T>(parameters);
        }
        catch
        {
            return default;
        }
    }


    public static object ResolveByNamed(this IServiceProvider serviceProvider, Type type, string named)
    {
        try
        {
            var context = serviceProvider.GetService<IComponentContext>();
            if (context == null)
                return default;

            return context.ResolveNamed(named, type);
        }
        catch
        {
            return default;
        }
    }

    public static object ResolveByNamed(this IServiceProvider serviceProvider, Type type, string named, params Parameter[] parameters)
    {
        try
        {
            var context = serviceProvider.GetService<IComponentContext>();
            if (context == null)
                return default;

            return context.ResolveNamed(named, type, parameters);
        }
        catch
        {
            return default;
        }
    }

    public static object ResolveByNamed(this IServiceProvider serviceProvider, Type type, string named, IEnumerable<Parameter> parameters)
    {
        try
        {
            var context = serviceProvider.GetService<IComponentContext>();
            if (context == null)
                return default;

            return context.ResolveNamed(named, type, parameters);
        }
        catch
        {
            return default;
        }
    }

    public static T ResolveByNamed<T>(this IServiceProvider serviceProvider, string named)
    {
        try
        {
            var context = serviceProvider.GetService<IComponentContext>();
            if (context == null)
                return default;

            return context.ResolveNamed<T>(named);
        }
        catch
        {
            return default;
        }
    }

    public static T ResolveByNamed<T>(this IServiceProvider serviceProvider, string named, params Parameter[] parameters)
    {
        try
        {
            var context = serviceProvider.GetService<IComponentContext>();
            if (context == null)
                return default;

            return context.ResolveNamed<T>(named, parameters);
        }
        catch
        {
            return default;
        }
    }

    public static T ResolveByNamed<T>(this IServiceProvider serviceProvider, string named, IEnumerable<Parameter> parameters)
    {
        try
        {
            var context = serviceProvider.GetService<IComponentContext>();
            if (context == null)
                return default;

            return context.ResolveNamed<T>(named, parameters);
        }
        catch
        {
            return default;
        }
    }


    public static object ResolveByKeyed(this IServiceProvider serviceProvider, Type type, object keyed)
    {
        try
        {
            var context = serviceProvider.GetService<IComponentContext>();
            if (context == null)
                return default;

            return context.ResolveKeyed(keyed, type);
        }
        catch
        {
            return default;
        }
    }

    public static object ResolveByKeyed(this IServiceProvider serviceProvider, Type type, object keyed, params Parameter[] parameters)
    {
        try
        {
            var context = serviceProvider.GetService<IComponentContext>();
            if (context == null)
                return default;

            return context.ResolveKeyed(keyed, type, parameters);
        }
        catch
        {
            return default;
        }
    }

    public static object ResolveByKeyed(this IServiceProvider serviceProvider, Type type, object keyed, IEnumerable<Parameter> parameters)
    {
        try
        {
            var context = serviceProvider.GetService<IComponentContext>();
            if (context == null)
                return default;

            return context.ResolveKeyed(keyed, type, parameters);
        }
        catch
        {
            return default;
        }
    }

    public static T ResolveByKeyed<T>(this IServiceProvider serviceProvider, object keyed)
    {
        try
        {
            var context = serviceProvider.GetService<IComponentContext>();
            if (context == null)
                return default;

            return context.ResolveKeyed<T>(keyed);
        }
        catch
        {
            return default;
        }
    }

    public static T ResolveByKeyed<T>(this IServiceProvider serviceProvider, object keyed, params Parameter[] parameters)
    {
        try
        {
            var context = serviceProvider.GetService<IComponentContext>();
            if (context == null)
                return default;

            return context.ResolveKeyed<T>(keyed, parameters);
        }
        catch
        {
            return default;
        }
    }

    public static T ResolveByKeyed<T>(this IServiceProvider serviceProvider, object keyed, IEnumerable<Parameter> parameters)
    {
        try
        {
            var context = serviceProvider.GetService<IComponentContext>();
            if (context == null)
                return default;

            return context.ResolveKeyed<T>(keyed, parameters);
        }
        catch
        {
            return default;
        }
    }
}