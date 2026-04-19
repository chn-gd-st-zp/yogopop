namespace DForge.Infrastructure.DTO;

/// <summary>
/// 系统设置
/// </summary>
public abstract class DTOSysSettingsUpdate : DTOSysSettingsInput
{
    /// <summary>
    /// ID
    /// </summary>
    [Required]
    public override string? PrimaryKey { get; set; }
}