namespace DForge.DSP;

/// <summary>
/// 域名主状态
/// </summary>
[PublicEnum]
public enum DMainStatusEnum
{
    /// <summary>
    /// 默认、无
    /// </summary>
    [Description("默认、无")]
    None,

    /// <summary>
    /// 正常
    /// </summary>
    [Description("正常")]
    Normal,

    /// <summary>
    /// 正常
    /// </summary>
    [Description("正常")]
    Disable,

    /// <summary>
    /// 处理中
    /// </summary>
    [Description("处理中")]
    Proceccing,

    /// <summary>
    /// 已过期
    /// </summary>
    [Description("已过期")]
    Expired,
}

/// <summary>
/// 域名子状态
/// </summary>
[PublicEnum]
public enum DSubStatusEnum
{
    /// <summary>
    /// 默认、无
    /// </summary>
    [Description("默认、无")]
    None,

    /// <summary>
    /// 域名服务更新中
    /// </summary>
    [Description("域名服务更新中")]
    NSUpdating,

    /// <summary>
    /// 域名解析中
    /// </summary>
    [Description("域名解析中")]
    Analysing,
}