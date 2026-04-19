namespace YogoPop.Component.MQ.RabbitMQ;

public abstract class RabbitMQHandle<TParam> : IMQHandle<TParam> where TParam : class, IMQParams
{
    /// <summary>
    /// 处理器状态
    /// </summary>
    public MQProcessEnum Status { get; set; } = MQProcessEnum.None;

    /// <summary>
    /// 配置
    /// </summary>
    private readonly RabbitMQSettings config;

    /// <summary>
    /// 链接工厂
    /// </summary>
    private readonly RabbitMQConnectionFactory factory;

    /// <summary>
    /// 服务连接
    /// </summary>
    private IConnection connection;

    /// <summary>
    /// 服务通道
    /// </summary>
    public IModel Channel { get; private set; }

    /// <summary>
    /// 处理器初始化
    /// </summary>
    public RabbitMQHandle()
    {
        config = InjectionContext.Resolve<RabbitMQSettings>();
        factory = InjectionContext.Resolve<RabbitMQConnectionFactory>();

        Init();

        Status = MQProcessEnum.Processing;
    }

    /// <summary>
    /// 连接初始化
    /// </summary>
    private void Init()
    {
        //获取连接
        connection = factory.GetConnection();

        //声明通道
        Channel = connection.CreateModel();
    }

    /// <summary>
    /// 处理器释放
    /// </summary>
    public void Dispose()
    {
        Status = MQProcessEnum.Finished;

        Channel.Dispose();
        factory.ReturnConnection(connection);
    }

    /// <summary>
    /// 运行
    /// </summary>
    /// <param name="eMQFunc"></param>
    /// <param name="mqParam"></param>
    public void Run(MQFuncEnum eMQFunc, TParam mqParam)
    {
        if (connection == null || !connection.IsOpen)
            Init();

        switch (eMQFunc)
        {
            case MQFuncEnum.Exec:
                Exec(mqParam);
                break;
        }
    }

    /// <summary>
    /// 执行队列
    /// </summary>
    /// <param name="mqParam"></param>
    protected abstract void Exec(TParam mqParam);
}