namespace YogoPop.Component.MQ.RabbitMQ;

public abstract class ObserveParams : RabbitMQParams
{
    /// <summary>
    /// 主题扩展
    /// </summary>
    public string TopicExt { get; set; } = "-";

    /// <summary>
    /// 交换机类型
    /// </summary>
    public ExchangerEnum ExchangeType => ExchangerEnum.Fanout;
}