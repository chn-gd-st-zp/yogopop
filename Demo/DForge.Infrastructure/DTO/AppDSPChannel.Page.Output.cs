namespace DForge.Infrastructure.DTO;

public class DTOAppDSPChannelPageResult : DTOPrimaryKey<string>
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
    /// 状态
    /// </summary>
    [Description("状态")]
    public StatusEnum Status { get; set; }

    /// <summary>
    /// 支持的 - 域名服务运行类型
    /// </summary>
    [Description("域名服务运行类型")]
    public DSOptEnum[] DSOpts => DSP.GetAttributes<DSOptAttribute>().Select(o => o.DSOpt).ToArray();

    /// <summary>
    /// 支持的 - 托管模式
    /// </summary>
    [Description("托管模式")]
    public string[] Trusteeships { get { return EnumExtension.ToEnumList<TrusteeshipEnum>().Select(o => o.ToString()).ToArray(); } }
}