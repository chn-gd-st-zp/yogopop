namespace YogoPop.Component.DataVisibility;

/// <summary>
/// 数据视野类型
/// </summary>
[Description("数据视野类型")]
[PublicEnum]
public enum DataVisionEnum
{
    /// <summary>
    /// 默认、无
    /// </summary>
    [Description("默认、无")]
    None = 0,

    /// <summary>
    /// 全部
    /// </summary>
    [Description("全部")]
    All = 1,

    /// <summary>
    /// 指定
    /// </summary>
    [Description("指定")]
    Specific = 2,
}