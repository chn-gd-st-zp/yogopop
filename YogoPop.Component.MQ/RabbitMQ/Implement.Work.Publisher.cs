namespace YogoPop.Component.MQ.RabbitMQ;

public class WorkPublisherParams : WorkParams, IPublisherParams
{
    /// <summary>
    /// 延迟发布毫秒数，使用此参数会使用到延迟交换机
    /// </summary>
    public int? Delay { get; set; }

    /// <summary>
    /// 是否消息持久化
    /// </summary>
    public bool Persistent { get; set; } = true;

    /// <summary>
    /// 优先级
    /// </summary>
    public PriorityEnum MsgPriority { get; set; } = PriorityEnum.Zero;

    /// <summary>
    /// 消息对象
    /// </summary>
    public List<IRabbitMQMessageEntity> MessageEntities { get; set; }
}

public class WorkPublisher : RabbitMQHandle<WorkPublisherParams>
{
    private IYogoLogger<WorkPublisher> _logger = InjectionContext.Resolve<IYogoLogger<WorkPublisher>>();

    protected override void Exec(WorkPublisherParams mqParam)
    {
        if (mqParam.MessageEntities.IsEmpty())
            return;

        var exchange = !mqParam.Delay.HasValue ? string.Empty : $"{mqParam.Topic}_ex_delay";
        var routingKey = $"{mqParam.Topic}_q";

        mqParam.MessageEntities.ForEach(async messageEntity =>
        {
            try
            {
                var properties = Channel.CreateBasicProperties();
                properties.Persistent = mqParam.Persistent;
                properties.Priority = (byte)(int)mqParam.MsgPriority;

                if (mqParam.Delay.HasValue)
                {
                    properties.Headers = new Dictionary<string, object>
                    {
                        // 设置延迟时间
                        { "x-delay", mqParam.Delay.Value }
                    };
                }

                Channel.BasicPublish(exchange, routingKey, properties, Encoding.UTF8.GetBytes(messageEntity.ToJson()));
            }
            catch (Exception ex)
            {
                _logger.Error($"{mqParam.Topic}", ex);
            }
        });
    }
}