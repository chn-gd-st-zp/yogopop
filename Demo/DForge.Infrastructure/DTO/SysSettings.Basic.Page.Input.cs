namespace DForge.Infrastructure.DTO;

public class DTOSysSettingsPage : DTOPager<DTOSort>
{
    /// <summary>
    /// 类型
    /// </summary>
    [Description("类型")]
    public SysSettingsEnum? Type { get; set; }
}