namespace YogoPop.Core.CusEnum;

/// <summary>
/// 时间周期
/// </summary>
[Description("时间周期")]
[PublicEnum]
public enum DateCycleEnum
{
    /// <summary>
    /// 默认、无
    /// </summary>
    [Description("默认、无")]
    None = 0,

    /// <summary>
    /// 天
    /// </summary>
    [Description("天")]
    Day,

    /// <summary>
    /// 周
    /// </summary>
    [Description("周")]
    Week,

    /// <summary>
    /// 月
    /// </summary>
    [Description("月")]
    Month,

    /// <summary>
    /// 季
    /// </summary>
    [Description("季")]
    Season,

    /// <summary>
    /// 年
    /// </summary>
    [Description("年")]
    Year,

    /// <summary>
    /// 时
    /// </summary>
    [Description("时")]
    Hour,

    /// <summary>
    /// 分
    /// </summary>
    [Description("分")]
    Minute,

    /// <summary>
    /// 秒
    /// </summary>
    [Description("秒")]
    Second,
}