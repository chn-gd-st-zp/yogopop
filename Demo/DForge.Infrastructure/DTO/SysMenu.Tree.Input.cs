namespace DForge.Infrastructure.DTO;

public class DTOSysMenuTree : DTOInput
{
    /// <summary>
    /// 系统类型
    /// </summary>
    [Description("系统类型")]
    [Required]
    public SysCategoryEnum Category { get; set; } = SysCategoryEnum.None;
}