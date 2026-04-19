namespace DForge.Infrastructure.DTO;

/// <summary>
/// 系统设置
/// </summary>
public abstract class DTOSysSettingsCreate : DTOSysSettingsInput
{
    [JsonIgnore, PropertyHidden]
    public override string? PrimaryKey { get; set; }
}