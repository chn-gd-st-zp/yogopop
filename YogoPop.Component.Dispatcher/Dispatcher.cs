namespace YogoPop.Component.Dispatcher;

public interface IDispatcher
{
    Task Run(string runnerName, params string[] args);
}

public abstract class Dispatcher : BackgroundService
{
    protected BackgroundWorker BackgroundWorker { get; private set; }

    public Dispatcher()
    {
        BackgroundWorker = new BackgroundWorker();
        BackgroundWorker.DoWork += (object sender, DoWorkEventArgs e) =>
        {
            while (!InjectionContext.IsDoneLoad)
                Thread.Sleep(1000);

            DoWork();
        };
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken) => BackgroundWorker.RunWorkerAsync();

    protected abstract void DoWork();
}