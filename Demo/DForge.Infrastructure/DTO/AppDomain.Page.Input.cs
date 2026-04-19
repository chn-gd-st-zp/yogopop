namespace DForge.Infrastructure.DTO;

public class DTOAppDomainPage : DTOPager<DTOSort>
{
    /// <summary>
    /// 项目ID
    /// </summary>
    [Description("项目ID")]
    public string? ProjectID { get; set; }

    /// <summary>
    /// 域名
    /// </summary>
    [Description("域名")]
    public string? Name { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    [Description("状态")]
    public StatusEnum? Status { get; set; }

    /// <summary>
    /// 创建时间区间
    /// </summary>
    [Description("创建时间区间")]
    public DTORange<DateTime?>? CreateTimeRange { get; set; }

    /// <summary>
    /// 过期时间区间
    /// </summary>
    [Description("过期时间区间")]
    public DTORange<DateTime?>? ExpiredTimeRange { get; set; }
}