namespace YogoPop.Component.MQ;

/// <summary>
/// 消息队列方法
/// </summary>
[Description("消息队列方法")]
public enum MQFuncEnum
{
    /// <summary>
    /// 默认、无
    /// </summary>
    [Description("默认、无")]
    None = 0,

    /// <summary>
    /// 发布、订阅
    /// </summary>
    [Description("发布、订阅")]
    Exec,
}