namespace YogoPop.Component.MQ.RabbitMQ;

/// <summary>
/// 优先级
/// </summary>
[Description("优先级")]
public enum PriorityEnum
{
    /// <summary>
    /// 默认、无
    /// </summary>
    [Description(" 默认、无")]
    None = -1,
    Zero = 0,
    //None,
    //Zero,
    One,
    Two,
    Three,
    Four,
    Five,
    Six,
    Seven,
    Eight,
    Nine,
}

/// <summary>
/// 交换机类型
/// </summary>
[Description("交换机类型")]
public enum ExchangerEnum
{
    /// <summary>
    /// 默认、无
    /// </summary>
    [Description("默认、无")]
    None = 0,

    /// <summary>
    /// 路由模式
    /// 如果路由键完全匹配的话,消息才会被投放到相应的队列
    /// </summary>
    [Description("路由模式")]
    Direct,

    /// <summary>
    /// 发布订阅模式
    /// 当发送一条消息到fanout交换器上时,它会把消息投放到所有附加在此交换器的上的队列
    /// </summary>
    [Description("发布订阅模式")]
    Fanout,

    /// <summary>
    /// 通配符模式
    /// 设置模糊的绑定方式,"*"操作符将"."视为分隔符,匹配单个字符;"#"操作符没有分块的概念,它将任意"."均视为关键字的匹配部分,能够匹配多个字符
    /// </summary>
    [Description("通配符模式")]
    Topic,

    /// <summary>
    /// 延迟模式
    /// </summary>
    [Description("延迟模式")]
    X_Delayed_Message,
}