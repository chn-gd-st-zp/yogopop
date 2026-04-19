namespace YogoPop.Support;

/// <summary>
/// 过滤器类型
/// </summary>
[Description("过滤器类型")]
public enum FilterTypeEnum
{
    /// <summary>
    /// 默认、无
    /// </summary>
    [Description("默认、无")]
    None = 0,

    /// <summary>
    /// 控制器
    /// </summary>
    [Description("控制器")]
    Ctrler,

    /// <summary>
    /// GRPC
    /// </summary>
    [Description("GRPC")]
    GRPC,
}