namespace YogoPop.Component.MQ.RabbitMQ;

public abstract class RouteParams : RabbitMQParams
{
    /// <summary>
    /// 主题扩展
    /// </summary>
    public string TopicExt { get; set; } = "-";

    /// <summary>
    /// 路由标识
    /// </summary>
    public string RoutingKey { get; set; } = "-";

    /// <summary>
    /// 交换机类型
    /// </summary>
    public ExchangerEnum ExchangeType => ExchangerEnum.Direct;
}