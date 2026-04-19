namespace DForge.Infrastructure.DTO;

public class DTOSysMenuSort : DTOInput
{
    /// <summary>
    /// 当前ID
    /// </summary>
    [Description("当前ID")]
    [Required]
    public string SourceID { get; set; } = string.Empty;

    /// <summary>
    /// 目标ID
    /// </summary>
    [Description("目标ID")]
    [Required]
    public string TargetID { get; set; } = string.Empty;
}