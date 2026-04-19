namespace DForge.DynScheduling;

public interface IDynSchActuator : ITransient, IDisposable
{
    public Task<bool> RunAsync(DynSchMQMsg msg);
}

public abstract class DynSchActuator<TActuator> : IDynSchActuator where TActuator : class, IDynSchActuator
{
    protected IServiceScope DIScope { get; private set; }

    protected IYogoLogger<TActuator> Logger { get; private set; }

    protected abstract string Tag { get; }

    public DynSchActuator()
    {
        DIScope = InjectionContext.Root.CreateScope();
        Logger = DIScope.Resolve<IYogoLogger<TActuator>>();
    }

    public void Dispose()
    {
        DIScope.Dispose();
    }

    public abstract Task<bool> RunAsync(DynSchMQMsg msg);
}