namespace YogoPop.Core.VHost;

/// <summary>
/// 运行器状态
/// </summary>
[Description("运行器状态")]
public enum VRunnerStateEnum
{
    /// <summary>
    /// 默认、无
    /// </summary>
    [Description("默认、无")]
    None = 0,

    /// <summary>
    /// 等待中
    /// </summary>
    [Description("等待中")]
    Waiting,

    /// <summary>
    /// 处理中
    /// </summary>
    [Description("处理中")]
    Processing,
}

public interface IVRunner
{
    /// <summary>
    /// 运行状态
    /// </summary>
    public VRunnerStateEnum RunningStatus { get; }

    /// <summary>
    /// 运行
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task Run(CancellationToken cancellationToken = default);

    /// <summary>
    /// 释放
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task Dispose(CancellationToken cancellationToken = default);
}

public abstract class VRunner<TTrigger> : IVRunner
    where TTrigger : class
{
    /// <summary>
    /// 日志记录器
    /// </summary>
    protected IYogoLogger<TTrigger> Logger { get; private set; }

    /// <summary>
    /// 运行状态
    /// </summary>
    public VRunnerStateEnum RunningStatus
    {
        get { return _runningStatus; }
        protected set
        {
            if (_runningStatus == value)
                return;

            UpdateStatus(value);

            _runningStatus = value;
        }
    }
    private VRunnerStateEnum _runningStatus = VRunnerStateEnum.None;

    /// <summary>
    /// 更新状态
    /// </summary>
    /// <param name="process"></param>
    protected virtual void UpdateStatus(VRunnerStateEnum process) { }

    /// <summary>
    /// 初始化
    /// </summary>
    protected abstract void Init();

    /// <summary>
    /// 运行
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task Run(CancellationToken cancellationToken)
    {
        string runnerName = typeof(TTrigger).Name;

        Info($"[{runnerName}]管道初始化...");
        while (InjectionContext.IsDoneLoad) Thread.Sleep(1000);
        Info($"[{runnerName}]管道初始化完成...");

        Info($"[{runnerName}]程序初始化...");
        Logger = InjectionContext.Resolve<IYogoLogger<TTrigger>>();
        RunningStatus = VRunnerStateEnum.Waiting;
        Init();
        Info($"[{runnerName}]程序初始化完成...");

        Info($"[{runnerName}]程序开始运行...");
        Execute(cancellationToken);
    }

    /// <summary>
    /// 释放
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public abstract Task Dispose(CancellationToken cancellationToken);

    /// <summary>
    /// 执行
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    protected abstract Task Execute(CancellationToken cancellationToken);

    /// <summary>
    /// 记录
    /// </summary>
    /// <param name="text"></param>
    protected void Info(string text)
    {
        Printor.PrintText(text);

        if (Logger != null)
            Logger.Info(text);
    }
}