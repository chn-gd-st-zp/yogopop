namespace DForge.Infrastructure.DTO;

public class AppDNSRecord : DTOPrimaryKey<string>, IDNSRecord
{
    /// <summary>
    /// 解析条目类型
    /// </summary>
    [Description("解析条目类型")]
    public DNSRecordEnum Type { get; set; } = DNSRecordEnum.None;

    /// <summary>
    /// 来源
    /// </summary>
    [Description("来源")]
    public string Source { get; set; } = string.Empty;

    /// <summary>
    /// 目标
    /// </summary>
    [Description("目标")]
    public string Target { get; set; } = string.Empty;

    /// <summary>
    /// TTL
    /// </summary>
    [Description("TTL")]
    public string TTL { get; set; } = string.Empty;

    /// <summary>
    /// Priority
    /// </summary>
    [Description("Priority")]
    public string Priority { get; set; } = string.Empty;

    /// <summary>
    /// Proxied
    /// </summary>
    [Description("Proxied")]
    public bool Proxied { get; set; } = default;

    /// <summary>
    /// IPv4Only
    /// </summary>
    [Description("IPv4Only")]
    public bool IPv4Only { get; set; } = default;

    /// <summary>
    /// IPv6Only
    /// </summary>
    [Description("IPv6Only")]
    public bool IPv6Only { get; set; } = default;

    /// <summary>
    /// 备注
    /// </summary>
    [Description("备注")]
    public string Remark { get; set; } = string.Empty;

    /// <summary>
    /// 标签集 - 通过,分割
    /// </summary>
    [Description("标签集")]
    public string Tags { get; set; } = string.Empty;

    /// <summary>
    /// 源ID
    /// </summary>
    [Description("源ID")]
    public string SrcID { get; set; } = string.Empty;
}