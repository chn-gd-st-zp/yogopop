namespace DForge.Infrastructure.DTO;

public class DTOSessionPage : DTOPager<DTOSort>
{
    /// <summary>
    /// 入口
    /// </summary>
    [Description("入口")]
    public EntryEnum? Entry { get; set; } = EntryEnum.None;

    /// <summary>
    /// 角色类型
    /// </summary>
    [Description("角色类型")]
    public RoleEnum? RoleType { get; set; } = RoleEnum.None;

    /// <summary>
    /// AccessToken
    /// </summary>
    [Description("AccessToken")]
    public string? AccessToken { get; set; }

    /// <summary>
    /// 账号
    /// </summary>
    [Description("账号")]
    public string? Account { get; set; }

    /// <summary>
    /// 创建时间区间
    /// </summary>
    [Description("创建时间区间")]
    public DTORange<DateTime?>? CreateTimeRange { get; set; }

    /// <summary>
    /// 更新时间区间
    /// </summary>
    [Description("更新时间区间")]
    public DTORange<DateTime?>? UpdateTimeRange { get; set; }

    /// <summary>
    /// 过期时间区间
    /// </summary>
    [Description("过期时间区间")]
    public DTORange<DateTime?>? ExpiredTimeRange { get; set; }
}