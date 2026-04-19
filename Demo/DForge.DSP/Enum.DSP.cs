namespace DForge.DSP;

/// <summary>
/// 域名服务供应商
/// </summary>
[PublicEnum]
public enum DSPEnum
{
    /// <summary>
    /// 默认、无
    /// </summary>
    [Description("默认、无")]
    None,

    /// <summary>
    /// GoDaddy
    /// </summary>
    [Description("GoDaddy")]
    [DSOpt(DSOptEnum.Regist)]
    GoDaddy,

    /// <summary>
    /// CloudFlare
    /// </summary>
    [Description("CloudFlare")]
    [DSOpt(DSOptEnum.Regist)]
    [DSOpt(DSOptEnum.Analyse)]
    CloudFlare,
}