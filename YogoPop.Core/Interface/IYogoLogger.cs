namespace YogoPop.Core.Interface;

public interface IYogoLogger : ISingleton
{
    public void Debug(string msg);

    public void Debug<T>(T obj);

    public void Info(string msg);

    public void Info<T>(T obj);

    public void Warn(string msg);

    public void Warn<T>(T obj);

    public void Error(Exception exception);

    public void Error(string msg, Exception exception = null);

    public void Error<T>(T obj, Exception exception = null);
}

public interface IYogoLogger<TTrigger> : IYogoLogger where TTrigger : class
{
    //
}

public interface IYogoLoggerEnricherFactory : ISingleton
{
    public IYogoLoggerEnricher[] Enrichers { get; }
}

public interface IYogoLoggerEnricher : ITransient
{
    public string Key { get; }
}