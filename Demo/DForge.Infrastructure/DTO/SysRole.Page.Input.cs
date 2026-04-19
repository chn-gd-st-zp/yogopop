namespace DForge.Infrastructure.DTO;

public class DTOSysRolePage : DTOPager<DTOSort>
{
    /// <summary>
    /// 名称
    /// </summary>
    [Description("名称")]
    public string? Name { get; set; }

    /// <summary>
    /// 类型
    /// </summary>
    [Description("类型")]
    public RoleEnum? Type { get; set; }

    /// <summary>
    /// 创建时间区间
    /// </summary>
    [Description("创建时间区间")]
    public DTORange<DateTime?>? CreateTimeRange { get; set; }
}