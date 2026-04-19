namespace DForge.Infrastructure.DTO;

public class DTOAppDSPChannelCreate : DTOInput
{
    /// <summary>
    /// 项目ID
    /// </summary>
    [Description("项目ID")]
    [Required]
    public string ProjectID { get; set; } = string.Empty;

    /// <summary>
    /// 域名服务供应商
    /// </summary>
    [Description("域名服务供应商")]
    [Required]
    public DSPEnum DSP { get; set; } = DSPEnum.None;

    /// <summary>
    /// 别名
    /// </summary>
    [Description("别名")]
    [Required]
    public string Alias { get; set; } = string.Empty;

    /// <summary>
    /// 配置 - Json格式，由开发提供
    /// </summary>
    [Description("配置")]
    [Required]
    public string Settings { get; set; } = string.Empty;
}