namespace YogoPop.Component.MQ.RabbitMQ;

public abstract class RabbitMQParams : IRabbitMQParams
{
    /// <summary>
    /// 主题
    /// </summary>
    public string Topic { get; set; } = string.Empty;

    /// <summary>
    /// 优先级
    /// </summary>
    public PriorityEnum Priority { get; set; } = PriorityEnum.Zero;

    /// <summary>
    /// 队列是否持久化
    /// </summary>
    public bool Durable { get; set; } = true;

    /// <summary>
    /// 队列是否自动删除
    /// 当最后一个consumer断开之后，autodelete被触发
    /// </summary>
    public bool AutoDelete { get; set; } = false;

    /// <summary>
    /// 是否排他队列
    /// 如果一个队列被声明为排他队列，该队列仅对首次声明它的连接可见，
    /// 并在连接断开时自动删除。这里需要注意三点: 其一，排他队列是基于连接可见的，同一连接的不同信道是可
    /// 以同时访问同一个连接创建的排他队列的。其二，“首次”，如果一个连接已经声明了一个排他队列，其他连
    /// 接是不允许建立同名的排他队列的，这个与普通队列不同。其三，即使该队列是持久化的，一旦连接关闭或者
    /// 客户端退出，该排他队列都会被自动删除的。这种队列适用于只限于一个客户端发送读取消息的应用场景。
    /// </summary>
    public bool Exclusive { get; set; } = false;
}