namespace DForge.Infrastructure.DTO;

public class DTOAppDNSRecordSet : DTOInput
{
    /// <summary>
    /// 通道ID
    /// </summary>
    [Description("通道ID")]
    [Required]
    public string ChannelID { get; set; } = string.Empty;

    /// <summary>
    /// 域名ID
    /// </summary>
    [Description("域名ID")]
    [Required]
    public string DomainID { get; set; } = string.Empty;

    /// <summary>
    /// DNS解析集
    /// </summary>
    [Description("DNS解析集")]
    [Required]
    public AppDNSRecord[] DNSRecords { get; set; } = default;
}