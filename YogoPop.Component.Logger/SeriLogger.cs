namespace YogoPop.Component.Logger;

//public interface ISeriLogger : IYogoLogger, Seri.ILogger { }

public interface ISeriLogger : IYogoLogger { }

public interface ISeriLogger<TTrigger> : IYogoLogger<TTrigger>, ISeriLogger where TTrigger : class { }

public class SeriLogger : ISeriLogger
{
    //protected Seri.ILogger Logger => Log.Logger;
    protected Seri.ILogger Logger { get; set; }

    public SeriLogger()
    {
        Logger = new LoggerConfiguration()
            .MinimumLevel.Verbose()
            .WriteTo.InclusiveLogger<SeriLogger>(LogEventLevel.Verbose)
            .WriteTo.InclusiveLogger<SeriLogger>(LogEventLevel.Debug)
            .WriteTo.InclusiveLogger<SeriLogger>(LogEventLevel.Information)
            .WriteTo.InclusiveLogger<SeriLogger>(LogEventLevel.Warning)
            .WriteTo.InclusiveLogger<SeriLogger>(LogEventLevel.Error)
            .WriteTo.InclusiveLogger<SeriLogger>(LogEventLevel.Fatal)
            .CreateLogger();
    }

    public void Debug(string msg) => Logger.Write(LogEventLevel.Debug, msg);

    public void Debug<T>(T obj) => Logger.Write(LogEventLevel.Debug, obj.ToJson());

    public void Info(string msg) => Logger.Write(LogEventLevel.Information, msg);

    public void Info<T>(T obj) => Logger.Write(LogEventLevel.Information, obj.ToJson());

    public void Warn(string msg) => Logger.Write(LogEventLevel.Warning, msg);

    public void Warn<T>(T obj) => Logger.Write(LogEventLevel.Warning, obj.ToJson());

    public void Error(Exception exception) => Logger.Write(LogEventLevel.Error, exception, exception.Message);

    public void Error(string msg, Exception exception = null) => Logger.Write(LogEventLevel.Error, exception, msg);

    public void Error<T>(T obj, Exception exception = null) => Logger.Write(LogEventLevel.Error, exception, obj.ToJson());

    #region SeriLogger

    /*
    public bool IsEnabled(LogEventLevel level) { return Logger.IsEnabled(level); }

    #region Bind

    public bool BindMessageTemplate(string messageTemplate, object[] propertyValues, out MessageTemplate parsedTemplate, out IEnumerable<LogEventProperty> boundProperties)
    { return Logger.BindMessageTemplate(messageTemplate, propertyValues, out parsedTemplate, out boundProperties); }

    public bool BindProperty(string propertyName, object value, bool destructureObjects, out LogEventProperty property)
    { return Logger.BindProperty(propertyName, value, destructureObjects, out property); }

    #endregion

    #region ForContext

    public Seri.ILogger ForContext(ILogEventEnricher enricher)
    { return Logger.ForContext(enricher); }

    public Seri.ILogger ForContext(IEnumerable<ILogEventEnricher> enrichers)
    { return Logger.ForContext(enrichers); }

    public Seri.ILogger ForContext(string propertyName, object value, bool destructureObjects = false)
    { return Logger.ForContext(propertyName, value, destructureObjects); }

    public Seri.ILogger ForContext<TSource>()
    { return Logger.ForContext<TSource>(); }

    public Seri.ILogger ForContext(Type source)
    { return Logger.ForContext(source); }

    #endregion

    #region Write

    public void Write(LogEvent logEvent)
    { Logger.Write(logEvent); this.ToEmail(logEvent.Level, logEvent.MessageTemplate.Text); }

    public void Write(LogEventLevel level, string messageTemplate)
    { Logger.Write(level, messageTemplate); this.ToEmail(level, messageTemplate); }

    public void Write(LogEventLevel level, string messageTemplate, params object[] propertyValues)
    { Logger.Write(level, messageTemplate, propertyValues); this.ToEmail(level, messageTemplate); }

    public void Write<T>(LogEventLevel level, string messageTemplate, T propertyValue)
    { Logger.Write(level, messageTemplate, propertyValue); this.ToEmail(level, messageTemplate); }

    public void Write<T0, T1>(LogEventLevel level, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
    { Logger.Write(level, messageTemplate, propertyValue0, propertyValue1); this.ToEmail(level, messageTemplate); }

    public void Write<T0, T1, T2>(LogEventLevel level, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
    { Logger.Write(level, messageTemplate, propertyValue0, propertyValue1, propertyValue2); this.ToEmail(level, messageTemplate); }

    public void Write(LogEventLevel level, Exception exception, string messageTemplate)
    { Logger.Write(level, exception, messageTemplate); this.ToEmail(level, messageTemplate, exception); }

    public void Write(LogEventLevel level, Exception exception, string messageTemplate, params object[] propertyValues)
    { Logger.Write(level, exception, messageTemplate, propertyValues); this.ToEmail(level, messageTemplate, exception); }

    public void Write<T>(LogEventLevel level, Exception exception, string messageTemplate, T propertyValue)
    { Logger.Write(level, exception, messageTemplate, propertyValue); this.ToEmail(level, messageTemplate, exception); }

    public void Write<T0, T1>(LogEventLevel level, Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
    { Logger.Write(level, exception, messageTemplate, propertyValue0, propertyValue1); this.ToEmail(level, messageTemplate, exception); }

    public void Write<T0, T1, T2>(LogEventLevel level, Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
    { Logger.Write(level, exception, messageTemplate, propertyValue0, propertyValue1, propertyValue2); this.ToEmail(level, messageTemplate, exception); }

    #endregion

    #region Verbose

    public void Verbose(string messageTemplate)
    { Logger.Verbose(messageTemplate); this.ToEmail(LogEventLevel.Verbose, messageTemplate); }

    public void Verbose(string messageTemplate, params object[] propertyValues)
    { Logger.Verbose(messageTemplate, propertyValues); this.ToEmail(LogEventLevel.Verbose, messageTemplate); }

    public void Verbose<T>(string messageTemplate, T propertyValue)
    { Logger.Verbose(messageTemplate, propertyValue); this.ToEmail(LogEventLevel.Verbose, messageTemplate); }

    public void Verbose<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
    { Logger.Verbose(messageTemplate, propertyValue0, propertyValue1); this.ToEmail(LogEventLevel.Verbose, messageTemplate); }

    public void Verbose<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
    { Logger.Verbose(messageTemplate, propertyValue0, propertyValue1, propertyValue2); this.ToEmail(LogEventLevel.Verbose, messageTemplate); }

    public void Verbose(Exception exception, string messageTemplate)
    { Logger.Verbose(exception, messageTemplate); this.ToEmail(LogEventLevel.Verbose, messageTemplate, exception); }

    public void Verbose(Exception exception, string messageTemplate, params object[] propertyValues)
    { Logger.Verbose(exception, messageTemplate, propertyValues); this.ToEmail(LogEventLevel.Verbose, messageTemplate, exception); }

    public void Verbose<T>(Exception exception, string messageTemplate, T propertyValue)
    { Logger.Verbose(exception, messageTemplate, propertyValue); this.ToEmail(LogEventLevel.Verbose, messageTemplate, exception); }

    public void Verbose<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
    { Logger.Verbose(exception, messageTemplate, propertyValue0, propertyValue1); this.ToEmail(LogEventLevel.Verbose, messageTemplate, exception); }

    public void Verbose<T0, T1, T2>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
    { Logger.Verbose(exception, messageTemplate, propertyValue0, propertyValue1, propertyValue2); this.ToEmail(LogEventLevel.Verbose, messageTemplate, exception); }

    #endregion

    #region Debug

    public void Debug(string messageTemplate)
    { Logger.Debug(messageTemplate); this.ToEmail(LogEventLevel.Debug, messageTemplate); }

    public void Debug(string messageTemplate, params object[] propertyValues)
    { Logger.Debug(messageTemplate, propertyValues); this.ToEmail(LogEventLevel.Debug, messageTemplate); }

    public void Debug<T>(string messageTemplate, T propertyValue)
    { Logger.Debug(messageTemplate, propertyValue); this.ToEmail(LogEventLevel.Debug, messageTemplate); }

    public void Debug<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
    { Logger.Debug(messageTemplate, propertyValue0, propertyValue1); this.ToEmail(LogEventLevel.Debug, messageTemplate); }

    public void Debug<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
    { Logger.Debug(messageTemplate, propertyValue0, propertyValue1, propertyValue2); this.ToEmail(LogEventLevel.Debug, messageTemplate); }

    public void Debug(Exception exception, string messageTemplate)
    { Logger.Debug(exception, messageTemplate); this.ToEmail(LogEventLevel.Debug, messageTemplate, exception); }

    public void Debug(Exception exception, string messageTemplate, params object[] propertyValues)
    { Logger.Debug(exception, messageTemplate, propertyValues); this.ToEmail(LogEventLevel.Debug, messageTemplate, exception); }

    public void Debug<T>(Exception exception, string messageTemplate, T propertyValue)
    { Logger.Debug(exception, messageTemplate, propertyValue); this.ToEmail(LogEventLevel.Debug, messageTemplate, exception); }

    public void Debug<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
    { Logger.Debug(exception, messageTemplate, propertyValue0, propertyValue1); this.ToEmail(LogEventLevel.Debug, messageTemplate, exception); }

    public void Debug<T0, T1, T2>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
    { Logger.Debug(exception, messageTemplate, propertyValue0, propertyValue1, propertyValue2); this.ToEmail(LogEventLevel.Debug, messageTemplate, exception); }

    #endregion

    #region Information

    public void Information(string messageTemplate)
    { Logger.Information(messageTemplate); this.ToEmail(LogEventLevel.Information, messageTemplate); }

    public void Information(string messageTemplate, params object[] propertyValues)
    { Logger.Information(messageTemplate, propertyValues); this.ToEmail(LogEventLevel.Information, messageTemplate); }

    public void Information<T>(string messageTemplate, T propertyValue)
    { Logger.Information(messageTemplate, propertyValue); this.ToEmail(LogEventLevel.Information, messageTemplate); }

    public void Information<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
    { Logger.Information(messageTemplate, propertyValue0, propertyValue1); this.ToEmail(LogEventLevel.Information, messageTemplate); }

    public void Information<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
    { Logger.Information(messageTemplate, propertyValue0, propertyValue1, propertyValue2); this.ToEmail(LogEventLevel.Information, messageTemplate); }

    public void Information(Exception exception, string messageTemplate)
    { Logger.Information(exception, messageTemplate); this.ToEmail(LogEventLevel.Information, messageTemplate, exception); }

    public void Information(Exception exception, string messageTemplate, params object[] propertyValues)
    { Logger.Information(exception, messageTemplate, propertyValues); this.ToEmail(LogEventLevel.Information, messageTemplate, exception); }

    public void Information<T>(Exception exception, string messageTemplate, T propertyValue)
    { Logger.Information(exception, messageTemplate, propertyValue); this.ToEmail(LogEventLevel.Information, messageTemplate, exception); }

    public void Information<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
    { Logger.Information(exception, messageTemplate, propertyValue0, propertyValue1); this.ToEmail(LogEventLevel.Information, messageTemplate, exception); }

    public void Information<T0, T1, T2>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
    { Logger.Information(exception, messageTemplate, propertyValue0, propertyValue1, propertyValue2); this.ToEmail(LogEventLevel.Information, messageTemplate, exception); }

    #endregion

    #region Warning

    public void Warning(string messageTemplate)
    { Logger.Warning(messageTemplate); this.ToEmail(LogEventLevel.Warning, messageTemplate); }

    public void Warning(string messageTemplate, params object[] propertyValues)
    { Logger.Warning(messageTemplate, propertyValues); this.ToEmail(LogEventLevel.Warning, messageTemplate); }

    public void Warning<T>(string messageTemplate, T propertyValue)
    { Logger.Warning(messageTemplate, propertyValue); this.ToEmail(LogEventLevel.Warning, messageTemplate); }

    public void Warning<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
    { Logger.Warning(messageTemplate, propertyValue0, propertyValue1); this.ToEmail(LogEventLevel.Warning, messageTemplate); }

    public void Warning<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
    { Logger.Warning(messageTemplate, propertyValue0, propertyValue1, propertyValue2); this.ToEmail(LogEventLevel.Warning, messageTemplate); }

    public void Warning(Exception exception, string messageTemplate)
    { Logger.Warning(exception, messageTemplate); this.ToEmail(LogEventLevel.Warning, messageTemplate, exception); }

    public void Warning(Exception exception, string messageTemplate, params object[] propertyValues)
    { Logger.Warning(exception, messageTemplate, propertyValues); this.ToEmail(LogEventLevel.Warning, messageTemplate, exception); }

    public void Warning<T>(Exception exception, string messageTemplate, T propertyValue)
    { Logger.Warning(exception, messageTemplate, propertyValue); this.ToEmail(LogEventLevel.Warning, messageTemplate, exception); }

    public void Warning<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
    { Logger.Warning(exception, messageTemplate, propertyValue0, propertyValue1); this.ToEmail(LogEventLevel.Warning, messageTemplate, exception); }

    public void Warning<T0, T1, T2>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
    { Logger.Warning(exception, messageTemplate, propertyValue0, propertyValue1, propertyValue2); this.ToEmail(LogEventLevel.Warning, messageTemplate, exception); }

    #endregion

    #region Error

    public void Error(string messageTemplate)
    { Logger.Error(messageTemplate); this.ToEmail(LogEventLevel.Error, messageTemplate); }

    public void Error(string messageTemplate, params object[] propertyValues)
    { Logger.Error(messageTemplate, propertyValues); this.ToEmail(LogEventLevel.Error, messageTemplate); }

    public void Error<T>(string messageTemplate, T propertyValue)
    { Logger.Error(messageTemplate, propertyValue); this.ToEmail(LogEventLevel.Error, messageTemplate); }

    public void Error<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
    { Logger.Error(messageTemplate, propertyValue0, propertyValue1); this.ToEmail(LogEventLevel.Error, messageTemplate); }

    public void Error<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
    { Logger.Error(messageTemplate, propertyValue0, propertyValue1, propertyValue2); this.ToEmail(LogEventLevel.Error, messageTemplate); }

    public void Error(Exception exception, string messageTemplate)
    { Logger.Error(exception, messageTemplate); this.ToEmail(LogEventLevel.Error, messageTemplate, exception); }

    public void Error(Exception exception, string messageTemplate, params object[] propertyValues)
    { Logger.Error(exception, messageTemplate, propertyValues); this.ToEmail(LogEventLevel.Error, messageTemplate, exception); }

    public void Error<T>(Exception exception, string messageTemplate, T propertyValue)
    { Logger.Error(exception, messageTemplate, propertyValue); this.ToEmail(LogEventLevel.Error, messageTemplate, exception); }

    public void Error<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
    { Logger.Error(exception, messageTemplate, propertyValue0, propertyValue1); this.ToEmail(LogEventLevel.Error, messageTemplate, exception); }

    public void Error<T0, T1, T2>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
    { Logger.Error(exception, messageTemplate, propertyValue0, propertyValue1, propertyValue2); this.ToEmail(LogEventLevel.Error, messageTemplate, exception); }

    #endregion

    #region Fatal

    public void Fatal(string messageTemplate)
    { Logger.Fatal(messageTemplate); this.ToEmail(LogEventLevel.Fatal, messageTemplate); }

    public void Fatal(string messageTemplate, params object[] propertyValues)
    { Logger.Fatal(messageTemplate, propertyValues); this.ToEmail(LogEventLevel.Fatal, messageTemplate); }

    public void Fatal<T>(string messageTemplate, T propertyValue)
    { Logger.Fatal(messageTemplate, propertyValue); this.ToEmail(LogEventLevel.Fatal, messageTemplate); }

    public void Fatal<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
    { Logger.Fatal(messageTemplate, propertyValue0, propertyValue1); this.ToEmail(LogEventLevel.Fatal, messageTemplate); }

    public void Fatal<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
    { Logger.Fatal(messageTemplate, propertyValue0, propertyValue1, propertyValue2); this.ToEmail(LogEventLevel.Fatal, messageTemplate); }

    public void Fatal(Exception exception, string messageTemplate)
    { Logger.Fatal(exception, messageTemplate); this.ToEmail(LogEventLevel.Fatal, messageTemplate, exception); }

    public void Fatal(Exception exception, string messageTemplate, params object[] propertyValues)
    { Logger.Fatal(exception, messageTemplate, propertyValues); this.ToEmail(LogEventLevel.Fatal, messageTemplate, exception); }

    public void Fatal<T>(Exception exception, string messageTemplate, T propertyValue)
    { Logger.Fatal(exception, messageTemplate, propertyValue); this.ToEmail(LogEventLevel.Fatal, messageTemplate, exception); }

    public void Fatal<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
    { Logger.Fatal(exception, messageTemplate, propertyValue0, propertyValue1); this.ToEmail(LogEventLevel.Fatal, messageTemplate, exception); }

    public void Fatal<T0, T1, T2>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
    { Logger.Fatal(exception, messageTemplate, propertyValue0, propertyValue1, propertyValue2); this.ToEmail(LogEventLevel.Fatal, messageTemplate, exception); }

    #endregion
    */

    #endregion
}

public class SeriLogger<TTrigger> : SeriLogger, ISeriLogger<TTrigger> where TTrigger : class
{
    public SeriLogger()
    {
        Logger = new LoggerConfiguration()
            .MinimumLevel.Verbose()
            .WriteTo.InclusiveLogger<TTrigger>(LogEventLevel.Verbose)
            .WriteTo.InclusiveLogger<TTrigger>(LogEventLevel.Debug)
            .WriteTo.InclusiveLogger<TTrigger>(LogEventLevel.Information)
            .WriteTo.InclusiveLogger<TTrigger>(LogEventLevel.Warning)
            .WriteTo.InclusiveLogger<TTrigger>(LogEventLevel.Error)
            .WriteTo.InclusiveLogger<TTrigger>(LogEventLevel.Fatal)
            .CreateLogger();
    }
}