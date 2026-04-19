namespace DForge.Core.Enum;

/// <summary>
/// 系统类型
/// </summary>
[PublicEnum]
public enum SysCategoryEnum
{
    /// <summary>
    /// 默认、无
    /// </summary>
    [Description("默认、无")]
    None,

    /// <summary>
    /// 系统后台
    /// </summary>
    [Description("系统后台")]
    SAdmin,

    /// <summary>
    /// 商户后台
    /// </summary>
    [Description("商户后台")]
    MAdmin,
}