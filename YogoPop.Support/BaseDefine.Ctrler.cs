namespace YogoPop.Support;

public class CtrlerBase : ControllerBase
{
    protected IYogoLogger Logger { get; private set; }

    public CtrlerBase()
    {
        Logger = InjectionContext.Resolve<IYogoLogger>();
    }
}

public class CtrlerBase<TLoggerType> : CtrlerBase
    where TLoggerType : class
{
    protected new IYogoLogger<TLoggerType> Logger { get; private set; }

    public CtrlerBase() : base()
    {
        Logger = InjectionContext.Resolve<IYogoLogger<TLoggerType>>();
    }
}

public class CtrlerBase<TLoggerType, TCache> : CtrlerBase<TLoggerType>
    where TLoggerType : class
    where TCache : ICache
{
    protected TCache Cache { get; private set; }

    public CtrlerBase() : base()
    {
        Cache = InjectionContext.Resolve<TCache>();
    }
}