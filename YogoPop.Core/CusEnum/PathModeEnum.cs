namespace YogoPop.Core.CusEnum;

/// <summary>
/// 路径类型
/// </summary>
[Description("路径类型")]
public enum PathModeEnum
{
    /// <summary>
    /// 默认、无
    /// </summary>
    [Description("默认、无")]
    None = 0,

    /// <summary>
    /// 绝对路径
    /// </summary>
    [Description("绝对路径")]
    ABS,

    /// <summary>
    /// 相对路径
    /// </summary>
    [Description("相对路径")]
    REF,
}