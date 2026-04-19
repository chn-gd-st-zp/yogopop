namespace YogoPop.Core.CusEnum;

/// <summary>
/// 时间单位
/// </summary>
[Description("时间单位")]
[PublicEnum]
public enum TimeUnitEnum
{
    /// <summary>
    /// 默认、无
    /// </summary>
    [Description("默认、无")]
    None = 0,

    /// <summary>
    /// 小时
    /// </summary>
    [Description("小时")]
    Hour,

    /// <summary>
    /// 分钟
    /// </summary>
    [Description("分钟")]
    Minute,

    /// <summary>
    /// 秒
    /// </summary>
    [Description("秒")]
    Second,
}