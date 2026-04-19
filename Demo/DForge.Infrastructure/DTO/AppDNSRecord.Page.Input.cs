namespace DForge.Infrastructure.DTO;

public class DTOAppDNSRecordPage : DTOPager<DTOSort>
{
    /// <summary>
    /// 域名ID
    /// </summary>
    [Description("域名ID")]
    public string DomainID { get; set; } = string.Empty;

    /// <summary>
    /// 解析条目类型
    /// </summary>
    [Description("解析条目类型")]
    public DNSRecordEnum? Type { get; set; } = DNSRecordEnum.None;
}