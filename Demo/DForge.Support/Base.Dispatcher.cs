namespace DForge.Support;

public abstract class DomainForgeDispatcher<TLoggerTrigger> : ITransient where TLoggerTrigger : class, IDispatcher
{
    private IYogoLogger<TLoggerTrigger> _logger;
    protected string RunnerName { get; set; }
    protected string[] Args { get; set; }

    public DomainForgeDispatcher() { _logger = InjectionContext.Resolve<IYogoLogger<TLoggerTrigger>>(); }

    protected void Debug(string message)
    {
        var obj = new
        {
            RunnerName,
            Args,
            Msg = message,
        };

        _logger.Info(obj.ToJson());
    }

    protected void Info(string message)
    {
        var obj = new
        {
            RunnerName,
            Args,
            Msg = message,
        };

        _logger.Info(obj.ToJson());
    }

    protected void Error(string message, Exception ex = null)
    {
        var obj = new
        {
            RunnerName,
            Args,
            Msg = message,
        };

        if (ex == null)
            _logger.Error(obj.ToJson());
        else
            _logger.Error(obj.ToJson(), ex);
    }

    protected void Error(object message, Exception ex = null)
    {
        var obj = new
        {
            RunnerName,
            Args,
            Msg = message,
        };

        if (ex == null)
            _logger.Error(obj.ToJson());
        else
            _logger.Error(obj.ToJson(), ex);
    }
}