namespace YogoPop.Support;

public class ServiceBase : IYogoService
{
    protected IYogoLogger Logger { get; private set; }

    public ServiceBase()
    {
        Logger = InjectionContext.Resolve<IYogoLogger>();
    }
}

public class ServiceBase<TService> : ServiceBase, IYogoService<TService>
    where TService : class, IYogoService
{
    protected new IYogoLogger<TService> Logger { get; private set; }

    public ServiceBase()
    {
        Logger = InjectionContext.Resolve<IYogoLogger<TService>>();
    }
}

public class ServiceBase<TService, TCache> : ServiceBase<TService>, IYogoService<TService, TCache>
    where TService : class, IYogoService
    where TCache : ICache
{
    public TCache Cache => InjectionContext.Resolve<TCache>();
}

public class ServiceBase<TService, TCache, TTokenProvider> : ServiceBase<TService, TCache>, IYogoService<TService, TCache, TTokenProvider>
    where TService : class, IYogoService
    where TCache : ICache
    where TTokenProvider : ITokenProvider
{
    public IYogoSession<TTokenProvider> Session { get; private set; }

    public ServiceBase()
    {
        Session = InjectionContext.Resolve<IYogoSession<TTokenProvider>>();
    }
}