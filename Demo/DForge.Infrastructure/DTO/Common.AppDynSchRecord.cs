namespace DForge.Infrastructure.DTO;

public abstract class DTOAppDynSchRecordPage : DTOPager<DTOSort>, IDTOPrimaryKey<string>
{
    public abstract DynSchEnum[] DynSchs { get; }

    public abstract string PrimaryKey { get; set; }
}

public class DTOAppDynSchRecordPageResult : DTOPrimaryKey<string>
{
    /// <summary>
    /// 是否手动运行的
    /// </summary>
    [Description("是否手动运行的")]
    public bool IsManual { get; set; } = false;

    /// <summary>
    /// 是否运行成功
    /// </summary>
    [Description("是否运行成功")]
    public bool IsSuccess { get; set; } = false;

    /// <summary>
    /// 主类型
    /// </summary>
    [Description("主类型")]
    public DynSchEnum MainType { get; set; } = DynSchEnum.None;

    /// <summary>
    /// 子类型
    /// </summary>
    [Description("子类型")]
    public string SubType { get; set; } = string.Empty;

    /// <summary>
    /// 频率
    /// </summary>
    [Description("频率")]
    public DateCycleEnum Frequency { get; set; } = DateCycleEnum.None;

    /// <summary>
    /// 数据时间
    /// </summary>
    [Description("数据时间")]
    public DateTime DataDate { get; set; } = DateTimeExtension.Now;

    /// <summary>
    /// 创建时间
    /// </summary>
    [Description("创建时间")]
    public DateTime CreateTime { get; set; } = DateTimeExtension.Now;

    /// <summary>
    /// 开始于
    /// </summary>
    [Description("开始于")]
    public DateTime StartAt { get; set; } = DateTimeExtension.Now;

    /// <summary>
    /// 结束于
    /// </summary>
    [Description("结束于")]
    public DateTime EndAt { get; set; } = DateTimeExtension.Now;
}
