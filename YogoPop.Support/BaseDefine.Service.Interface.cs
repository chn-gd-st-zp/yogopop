namespace YogoPop.Support;

public interface IYogoServiceWithTrigger<TService> : IYogoService
    where TService : class, IYogoService
{
    //
}

public interface IYogoServiceWithCache<TCache> : IYogoService
    where TCache : ICache
{
    //
}

public interface IYogoServiceWithTokenProvider<TTokenProvider> : IYogoService
    where TTokenProvider : ITokenProvider
{
    //
}

public interface IYogoService<TService> : IYogoServiceWithTrigger<TService>
    where TService : class, IYogoService
{
    //
}

public interface IYogoService<TService, TCache> : IYogoService<TService>, IYogoServiceWithTrigger<TService>, IYogoServiceWithCache<TCache>
    where TService : class, IYogoService
    where TCache : ICache
{
    TCache Cache { get; }
}

public interface IYogoService<TService, TCache, TTokenProvider> : IYogoService<TService, TCache>, IYogoServiceWithTrigger<TService>, IYogoServiceWithCache<TCache>, IYogoServiceWithTokenProvider<TTokenProvider>
    where TService : class, IYogoService
    where TCache : ICache
    where TTokenProvider : ITokenProvider
{
    IYogoSession<TTokenProvider> Session { get; }
}