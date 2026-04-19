namespace DForge.Infrastructure.DTO;

public class DTOSysSettingsList : DTOListor<DTOSort>
{
    /// <summary>
    /// 类型
    /// </summary>
    [Description("类型")]
    [JsonIgnore, PropertyHidden]
    public virtual SysSettingsEnum? Type { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    [Description("状态")]
    [JsonIgnore, PropertyHidden]
    public StatusEnum? Status { get; set; }
}