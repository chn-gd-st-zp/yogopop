namespace YogoPop.Component.Logger;

public interface INLogger : IYogoLogger { }

public interface INLogger<TTrigger> : INLogger, IYogoLogger<TTrigger> where TTrigger : class { }

public class NLogger : INLogger
{
    private NLog.ILogger _logger;

    public NLogger() => _logger = LogManager.GetCurrentClassLogger();

    public void Debug(string msg) => _logger.Debug(msg);

    public void Debug<T>(T obj) => Debug(obj.ToJson());

    public void Info(string msg) => _logger.Info(msg);

    public void Info<T>(T obj) => Info(obj.ToJson());

    public void Warn(string msg) => _logger.Warn(msg);

    public void Warn<T>(T obj) => Warn(obj.ToJson());

    public void Error(Exception exception) => _logger.Error(exception);

    public void Error(string msg, Exception exception = null)
    {
        exception = exception != null ? exception : new Exception();
        var errorObj = new { ErrorMsg = msg, ErrorInfo = exception.Message, ErrorTrace = exception.StackTrace };
        _logger.Error(exception, errorObj.ToJson());
    }

    public void Error<T>(T obj, Exception exception = null)
    {
        exception = exception != null ? exception : new Exception();
        var errorObj = new { ErrorObj = obj, ErrorInfo = exception.Message, ErrorTrace = exception.StackTrace };
        _logger.Error(exception, errorObj.ToJson());
    }
}

public class NLogger<TTrigger> : INLogger<TTrigger>
    where TTrigger : class
{
    private ILogger<TTrigger> _logger;

    public NLogger() => _logger = InjectionContext.Resolve<ILogger<TTrigger>>();

    public void Debug(string msg) => _logger.LogDebug(msg);

    public void Debug<T>(T obj) => Debug(obj.ToJson());

    public void Info(string msg) => _logger.LogInformation(msg);

    public void Info<T>(T obj) => Info(obj.ToJson());

    public void Warn(string msg) => _logger.LogWarning(msg);

    public void Warn<T>(T obj) => Warn(obj.ToJson());

    public void Error(Exception exception) => _logger.LogError(exception, exception.Message);

    public void Error(string msg, Exception exception = null)
    {
        exception = exception != null ? exception : new Exception();
        var errorObj = new { ErrorMsg = msg, ErrorInfo = exception.Message, ErrorTrace = exception.StackTrace };
        _logger.LogError(exception, errorObj.ToJson());
    }

    public void Error<T>(T obj, Exception exception = null)
    {
        exception = exception != null ? exception : new Exception();
        var errorObj = new { ErrorObj = obj, ErrorInfo = exception.Message, ErrorTrace = exception.StackTrace };
        _logger.LogError(exception, errorObj.ToJson());
    }
}