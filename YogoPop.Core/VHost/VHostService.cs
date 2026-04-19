namespace YogoPop.Core.VHost;

public class VHostService : IHostedService
{
    protected IVRunner Runner;

    protected BackgroundWorker BackgroundWorker { get; private set; }

    public VHostService(IVRunner runner)
    {
        Runner = runner;
        BackgroundWorker = new BackgroundWorker();
        BackgroundWorker.DoWork += (object sender, DoWorkEventArgs e) => { Runner.Run((CancellationToken)e.Argument); };
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        BackgroundWorker.RunWorkerAsync(cancellationToken);
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await Runner.Dispose(cancellationToken);
        BackgroundWorker.Dispose();
    }
}

public class VHostService<TVRunner> : VHostService
    where TVRunner : IVRunner
{
    public VHostService(TVRunner runner) : base(runner) { }
}