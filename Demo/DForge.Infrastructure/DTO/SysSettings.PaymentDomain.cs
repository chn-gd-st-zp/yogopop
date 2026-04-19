namespace DForge.Infrastructure.DTO;

/// <summary>
/// 系统设置 - 支付域名
/// </summary>
public class DTOPaymentDomainResult : DTOSysSettingsResult
{
    [JsonIgnore, PropertyHidden]
    public override SysSettingsEnum Type { get; set; }

    /// <summary>
    /// 替换标识
    /// </summary>
    [Description("替换标识")]
    public string Tag { get; set; }

    /// <summary>
    /// 测试地址
    /// </summary>
    [Description("测试地址")]
    public string Test { get; set; }

    /// <summary>
    /// 生产地址
    /// </summary>
    [Description("生产地址")]
    public string Live { get; set; }
}