namespace DForge.Infrastructure.DBSupport.Entity.Table;

[Table("TB_App_DynSchRecord")]
[DefaultSort("CreateTime", SortDirectionEnum.DESC)]
public partial class TBAppDynSchRecord : IDBFPrimaryKey<long>, IDynSchRecordEntity
{
    [Column("ID")]
    [Sort("ID")]
    public long PrimaryKey { get; set; } = long.Parse(Unique.GetID());

    [Column("TriggerID")]
    [Sort("TriggerID")]
    public string TriggerID { get; set; } = string.Empty;

    [Column("IsManual")]
    [Sort("IsManual")]
    public bool IsManual { get; set; } = false;

    [Column("MainType")]
    [Sort("MainType")]
    public DynSchEnum MainType { get; set; } = DynSchEnum.None;

    [Column("SubType")]
    [Sort("SubType")]
    public string SubType { get; set; } = string.Empty;

    [Column("Frequency")]
    [Sort("Frequency")]
    public DateCycleEnum Frequency { get; set; } = DateCycleEnum.None;

    [Column("CreateTime")]
    [Sort("CreateTime")]
    public DateTime CreateTime { get; set; } = DateTimeExtension.Now;

    [Column("DataDate")]
    [Sort("DataDate")]
    public DateTime DataDate { get; set; } = DateTimeExtension.Now;

    [Column("DataYear")]
    [Sort("DataYear")]
    public int DataYear { get; set; } = 0;

    [Column("DataMonth")]
    [Sort("DataMonth")]
    public int DataMonth { get; set; } = 0;

    [Column("DataDay")]
    [Sort("DataDay")]
    public int DataDay { get; set; } = 0;

    [Column("DataHour")]
    [Sort("DataHour")]
    public int DataHour { get; set; } = 0;

    [Column("DataMinute")]
    [Sort("DataMinute")]
    public int DataMinute { get; set; } = 0;

    [Column("DataSecond")]
    [Sort("DataSecond")]
    public int DataSecond { get; set; } = 0;

    [Column("StartAt")]
    [Sort("StartAt")]
    public DateTime StartAt { get; set; } = DateTimeExtension.Now;

    [Column("EndAt")]
    [Sort("EndAt")]
    public DateTime EndAt { get; set; } = DateTimeExtension.Now;

    [Column("IsSuccess")]
    [Sort("IsSuccess")]
    public bool IsSuccess { get; set; } = false;
}