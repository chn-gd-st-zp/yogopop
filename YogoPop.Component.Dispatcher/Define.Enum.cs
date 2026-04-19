namespace YogoPop.Component.Dispatcher;

/// <summary>
/// 调度器类型
/// </summary>
[Description("调度器类型")]
public enum DispatcherTypeEnum
{
    /// <summary>
    /// 默认、无
    /// </summary>
    [Description("默认、无")]
    None = 0,

    /// <summary>
    /// 后台任务
    /// </summary>
    [Description("后台任务")]
    Background,

    /// <summary>
    /// HangFire定时任务
    /// </summary>
    [Description("HangFire定时任务")]
    HangFireTiming,

    /// <summary>
    /// Quartz定时任务
    /// </summary>
    [Description("Quartz定时任务")]
    QuartzTiming,
}