namespace DForge.Infrastructure.DTO;

public class DTOAppDomainPageResult : DTOPrimaryKey<string>
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
    /// 注册商ID
    /// </summary>
    [Description("注册商ID")]
    public string RegistChannelID { get; set; } = string.Empty;

    /// <summary>
    /// 注册商别名
    /// </summary>
    [Description("注册商别名")]
    public string RegistChannelAlias { get; set; } = string.Empty;

    /// <summary>
    /// 解析商ID
    /// </summary>
    [Description("解析商ID")]
    public string AnalyseChannelID { get; set; } = string.Empty;

    /// <summary>
    /// 解析商别名
    /// </summary>
    [Description("解析商别名")]
    public string AnalyseChannelAlias { get; set; } = string.Empty;

    /// <summary>
    /// 域名
    /// </summary>
    [Description("域名")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// NS集 - 通过,分割
    /// </summary>
    [Description("NS集")]
    public string NameServers { get; set; } = string.Empty;

    /// <summary>
    /// 创建时间
    /// </summary>
    [Description("创建时间")]
    public DateTime? Createtime { get; set; } = default;

    /// <summary>
    /// 过期时间
    /// </summary>
    [Description("过期时间")]
    public DateTime? ExpiredTime { get; set; } = default;

    /// <summary>
    /// 域名主状态
    /// </summary>
    [Description("域名主状态")]
    public DMainStatusEnum MainStatus { get; set; } = DMainStatusEnum.None;

    /// <summary>
    /// 域名子状态
    /// </summary>
    [Description("域名子状态")]
    public DSubStatusEnum SubStatus { get; set; } = DSubStatusEnum.None;

    /// <summary>
    /// 状态
    /// </summary>
    [Description("状态")]
    public StatusEnum Status { get; set; } = StatusEnum.None;
}