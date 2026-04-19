namespace DForge.Infrastructure.DTO;

/// <summary>
/// 系统设置 - 支付域名
/// </summary>
public class DTOPaymentDomainEdit : DTOSysSettingsEdit
{
    [JsonIgnore, PropertyHidden]
    public override SysSettingsEnum Type { get { return SysSettingsEnum.PayDomain; } }

    [JsonIgnore, PropertyHidden]
    public override string? CurSequence { get; set; }

    /// <summary>
    /// 资源地址
    /// </summary>
    [JsonExtProp]
    [Description("资源地址")]
    [Required]
    public string Resource { get; set; }

    /// <summary>
    /// 替换标识
    /// </summary>
    [JsonExtProp]
    [Description("替换标识")]
    [Required]
    public string Tag { get; set; }

    /// <summary>
    /// 主页
    /// </summary>
    [JsonExtProp]
    [Description("主页")]
    [Required]
    public string Main { get; set; }

    /// <summary>
    /// 收银台
    /// </summary>
    [JsonExtProp]
    [Description("收银台")]
    [Required]
    public string Pay { get; set; }
}