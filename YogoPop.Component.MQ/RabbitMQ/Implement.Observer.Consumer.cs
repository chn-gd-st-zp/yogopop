namespace YogoPop.Component.MQ.RabbitMQ;

public class ObserveConsumerParams : ObserveParams, IConsumerParams
{
    /// <summary>
    /// 是否使用到延迟交换机
    /// </summary>
    public bool UseDelayEx { get; set; } = false;

    /// <summary>
    /// 读取数
    /// </summary>
    public int PrefetchCount { get; set; } = 1;

    /// <summary>
    /// 是否自动反馈
    /// </summary>
    public bool AutoAck { get; set; } = false;

    /// <summary>
    /// 业务方法
    /// </summary>
    public MQReceive BusinessFunc { get; set; }
}

public class ObserveConsumer : RabbitMQHandle<ObserveConsumerParams>
{
    protected override void Exec(ObserveConsumerParams mqParam)
    {
        if (mqParam.BusinessFunc == null)
            return;

        var arguments = new Dictionary<string, object>();

        if (mqParam.Priority != PriorityEnum.None)
            arguments["x-max-priority"] = (int)mqParam.Priority;

        var routingKey = $"{mqParam.Topic}_{mqParam.TopicExt}_q";
        var exchange = $"{mqParam.Topic}_ex";
        var exchange_delay = $"{mqParam.Topic}_ex_delay";

        //声明队列
        Channel.QueueDeclareNoWait(
            queue: routingKey,
            durable: mqParam.Durable,
            exclusive: mqParam.Exclusive,
            autoDelete: mqParam.AutoDelete,
            arguments: arguments
        );

        //声明交换器
        Channel.ExchangeDeclareNoWait(
            exchange: exchange,
            type: mqParam.ExchangeType.ToString().ToLower(),
            durable: mqParam.Durable,
            autoDelete: mqParam.AutoDelete,
            arguments: arguments
        );

        //绑定队列
        Channel.QueueBindNoWait(routingKey, exchange, string.Empty, arguments);

        if (mqParam.UseDelayEx)
        {
            // 声明延迟交换机
            Channel.ExchangeDeclareNoWait(
                exchange: exchange_delay,
                type: ExchangerEnum.X_Delayed_Message.ToString().ToLower().Replace("_", "-"),
                durable: mqParam.Durable,
                autoDelete: mqParam.AutoDelete,
                arguments: new Dictionary<string, object>
                {
                    // 延迟消息的路由类型
                    { "x-delayed-type", mqParam.ExchangeType.ToString().ToLower() }
                });

            // 绑定【队列】到【延迟交换机】
            Channel.QueueBind(routingKey, exchange_delay, string.Empty, arguments);
        }

        //客服端 向 服务器 索取的消息条目数
        Channel.BasicQos(0, Convert.ToUInt16(mqParam.PrefetchCount), false);

        //定义队列的消费者
        var consumer = new EventingBasicConsumer(Channel);
        consumer.Received += (sender, e) =>
        {
            mqParam.BusinessFunc(Channel, sender, e);
        };

        //监听队列
        Channel.BasicConsume(
            queue: routingKey,
            autoAck: mqParam.AutoAck,
            consumer: consumer
        );
    }
}