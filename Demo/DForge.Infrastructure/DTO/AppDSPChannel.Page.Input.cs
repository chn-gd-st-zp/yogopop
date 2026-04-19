namespace DForge.Infrastructure.DTO;

public class DTOAppDSPChannelPage : DTOPager<DTOSort>
{
    /// <summary>
    /// 关键字
    /// </summary>
    [Description("关键字")]
    public string? Keyword { get; set; } = string.Empty;

    /// <summary>
    /// 项目ID
    /// </summary>
    [Description("项目ID")]
    public string? ProjectID { get; set; } = string.Empty;

    /// <summary>
    /// 域名服务供应商
    /// </summary>
    [Description("域名服务供应商")]
    public DSPEnum? DSP { get; set; } = DSPEnum.None;

    /// <summary>
    /// 别名
    /// </summary>
    [Description("别名")]
    public string? Alias { get; set; } = string.Empty;

    /// <summary>
    /// 状态
    /// </summary>
    [Description("状态")]
    public StatusEnum? Status { get; set; }
}