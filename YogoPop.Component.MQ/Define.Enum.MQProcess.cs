namespace YogoPop.Component.MQ;

/// <summary>
/// 消息队列执行状态
/// </summary>
[Description("消息队列执行状态")]
public enum MQProcessEnum
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

    /// <summary>
    /// 重新处理中
    /// </summary>
    [Description("重新处理中")]
    ReProcessing,

    /// <summary>
    /// 待确认
    /// </summary>
    [Description("待确认")]
    Confirming,

    /// <summary>
    /// 完成
    /// </summary>
    [Description("完成")]
    Finished,

    /// <summary>
    /// 成功
    /// </summary>
    [Description("成功")]
    Success,

    /// <summary>
    /// 失败
    /// </summary>
    [Description("失败")]
    Fail,
}