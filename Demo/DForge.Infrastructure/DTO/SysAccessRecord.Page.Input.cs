namespace DForge.Infrastructure.DTO;

public class DTOSysAccessRecordPage : DTOPager<DTOSort>
{
    /// <summary>
    /// 关键字
    /// </summary>
    [Description("关键字")]
    public string? Keyword { get; set; }

    /// <summary>
    /// 账号ID
    /// </summary>
    [Description("账号ID")]
    public string? AccountID { get; set; }

    /// <summary>
    /// 用户名
    /// </summary>
    [Description("用户名")]
    public string? UserName { get; set; }

    /// <summary>
    /// 操作类型
    /// </summary>
    [Description("操作类型")]
    public OperationTypeEnum? OperationType { get; set; }

    /// <summary>
    /// 数据表实际名
    /// </summary>
    [Description("数据表实际名")]
    public string? TBName { get; set; }

    /// <summary>
    /// 对象主键值
    /// </summary>
    [Description("对象主键值")]
    public string? PKValue { get; set; }

    /// <summary>
    /// 创建时间区间
    /// </summary>
    [Description("创建时间区间")]
    public DTORange<DateTime?>? CreateTimeRange { get; set; }
}