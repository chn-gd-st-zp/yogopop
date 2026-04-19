namespace DForge.Infrastructure.DTO;

public class DTOAppDSPChannelSingleResult : DTOPrimaryKey<string>
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
    /// 域名服务供应商
    /// </summary>
    [Description("域名服务供应商")]
    public DSPEnum DSP { get; set; } = DSPEnum.None;

    /// <summary>
    /// 别名
    /// </summary>
    [Description("别名")]
    public string Alias { get; set; } = string.Empty;

    /// <summary>
    /// 配置 - Json格式，由开发提供
    /// </summary>
    [Description("配置")]
    public string Settings { get; set; } = string.Empty;

    /// <summary>
    /// 状态
    /// </summary>
    [Description("状态")]
    public StatusEnum Status { get; set; }
}