namespace DForge.DynScheduling;

public interface IDynSchRecordEntity : IDBEntity
{
    /// <summary>
    /// 是否手动运行的
    /// </summary>
    public bool IsManual { get; set; }

    /// <summary>
    /// 动态调度类型
    /// </summary>
    public DynSchEnum MainType { get; set; }

    /// <summary>
    /// 子类型
    /// </summary>
    public string SubType { get; set; }

    /// <summary>
    /// 频率
    /// </summary>
    public DateCycleEnum Frequency { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// 数据时间
    /// </summary>
    public DateTime DataDate { get; set; }

    /// <summary>
    /// 数据年
    /// </summary>
    public int DataYear { get; set; }

    /// <summary>
    /// 数据月
    /// </summary>
    public int DataMonth { get; set; }

    /// <summary>
    /// 数据日
    /// </summary>
    public int DataDay { get; set; }

    /// <summary>
    /// 开始于
    /// </summary>
    public DateTime StartAt { get; set; }

    /// <summary>
    /// 结束于
    /// </summary>
    public DateTime EndAt { get; set; }

    /// <summary>
    /// 是否运行成功
    /// </summary>
    public bool IsSuccess { get; set; }
}