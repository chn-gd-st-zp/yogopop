namespace DForge.DSP;

/// <summary>
/// 解析条目类型
/// </summary>
[PublicEnum]
public enum DNSRecordEnum
{
    /// <summary>
    /// 默认、无
    /// </summary>
    [Description("默认、无")]
    None = 0,

    /// <summary>
    /// A - Address record
    /// </summary>
    [Description("A")]
    A = 1,

    /// <summary>
    /// AAAA - IPv6 address record
    /// </summary>
    [Description("AAAA")]
    AAAA,

    /// <summary>
    /// ALIAS - CNAME flattening record
    /// </summary>
    [Description("ALIAS")]
    ALIAS,

    /// <summary>
    /// CAA - Certification Authority Authorization
    /// </summary>
    [Description("CAA")]
    CAA,

    /// <summary>
    /// CNAME - Canonical name record
    /// </summary>
    [Description("CNAME")]
    CNAME,

    /// <summary>
    /// HTTPS - HTTPS Service Record
    /// </summary>
    [Description("HTTPS")]
    HTTPS,

    /// <summary>
    /// MX - Mail exchange record
    /// </summary>
    [Description("MX")]
    MX,

    /// <summary>
    /// NS - Name server record
    /// </summary>
    [Description("NS")]
    NS,

    /// <summary>
    /// SRV - Service record
    /// </summary>
    [Description("SRV")]
    SRV,

    /// <summary>
    /// SVCB - Service Binding Record
    /// </summary>
    [Description("SVCB")]
    SVCB,

    /// <summary>
    /// TLSA - TLS Authentication Record
    /// </summary>
    [Description("TLSA")]
    TLSA,

    /// <summary>
    /// TXT - Text record
    /// </summary>
    [Description("TXT")]
    TXT,
}