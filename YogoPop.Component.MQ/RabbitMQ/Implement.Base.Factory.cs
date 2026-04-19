namespace YogoPop.Component.MQ.RabbitMQ;

[DIModeForService(DIModeEnum.AsSelf)]
public class RabbitMQConnectionFactory : ISingleton
{
    private readonly RabbitMQSettings config = InjectionContext.Resolve<RabbitMQSettings>();
    private readonly ConcurrentBag<IConnection> _connections = new ConcurrentBag<IConnection>();
    private readonly ConnectionFactory _connectionFactory;
    private int _currentSize = 0;

    public RabbitMQConnectionFactory()
    {
        _connectionFactory = new ConnectionFactory()
        {
            UserName = config.UserName,
            Password = config.Password,
            HostName = config.HostName,
            Port = int.Parse(config.Port),
            VirtualHost = config.VirtualHost,
            RequestedHeartbeat = TimeSpan.FromSeconds(config.Heartbeat),
            AutomaticRecoveryEnabled = true
        };
    }

    // 获取连接
    public IConnection GetConnection()
    {
        if (config.PoolSize == default)
            return _connectionFactory.CreateConnection();

        if (_connections.TryTake(out var connection) && connection.IsOpen)
            return connection;

        lock (this)
        {
            if (_currentSize < config.PoolSize)
            {
                connection = _connectionFactory.CreateConnection();
                Interlocked.Increment(ref _currentSize);
                return connection;
            }
        }

        // 如果连接池已达到最大尺寸，阻塞或抛出异常
        throw new InvalidOperationException("RabbitMQ connection pool is exhausted.");
    }

    // 归还连接
    public void ReturnConnection(IConnection connection)
    {
        if (config.PoolSize == default)
        {
            if (connection != null && connection.IsOpen)
                connection.Dispose();

            return;
        }

        if (connection != null && connection.IsOpen)
        {
            _connections.Add(connection);
        }
        else
        {
            lock (this)
            {
                Interlocked.Decrement(ref _currentSize);
            }
        }
    }

    // 关闭所有连接
    public void CloseAll()
    {
        while (_connections.TryTake(out var connection))
        {
            connection.Close();
        }
    }
}