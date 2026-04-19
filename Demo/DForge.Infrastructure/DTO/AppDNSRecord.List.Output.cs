namespace DForge.Infrastructure.DTO;

public class DTOAppDNSRecordListResult : AppDNSRecord
{
    /// <summary>
    /// 项目ID
    /// </summary>
    [Description("项目ID")]
    public string ProjectID { get; set; } = string.Empty;

    /// <summary>
    /// 项目名称
    /// </summary>
    [Description("项目名称")]
    public string ProjectName { get; set; } = string.Empty;

    /// <summary>
    /// 域名ID
    /// </summary>
    [Description("域名ID")]
    public string DomainID { get; set; } = string.Empty;

    /// <summary>
    /// 域名名称
    /// </summary>
    [Description("域名名称")]
    public string DomainName { get; set; } = string.Empty;
}