namespace DForge.DynScheduling;

/// <summary>
/// 动态调度类型
/// </summary>
[PublicEnum]
public enum DynSchEnum
{
    /// <summary>
    /// 默认、无
    /// </summary>
    [Description("默认、无")]
    None,

    /// <summary>
    /// 域名同步
    /// </summary>
    [Description("域名同步")]
    [DynSchPeriod(DateCycleEnum.Day, "0 0 18 * * ?")]
    DomainSync,

    /// <summary>
    /// NS同步
    /// </summary>
    [Description("NS同步")]
    NameServerSync,

    /// <summary>
    /// DNS同步
    /// </summary>
    [Description("DNS同步")]
    DNSSync,

    /// <summary>
    /// 解析设置
    /// </summary>
    [Description("解析同步")]
    AnalyseSync,
}