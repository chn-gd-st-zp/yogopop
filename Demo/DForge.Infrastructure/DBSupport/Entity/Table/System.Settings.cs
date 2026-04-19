namespace DForge.Infrastructure.DBSupport.Entity.Table;

[Table("TB_Sys_Settings")]
[DefaultSort("Sequence", SortDirectionEnum.ASC)]
public partial class TBSysSettings : DBFStatus, IDBFPrimaryKey<string>, IDBFJson, IDBFSequence
{
    [Column("ID")]
    [Sort("ID")]
    public string PrimaryKey { get; set; } = Unique.GetID();

    [Column("Type")]
    [Sort("Type")]
    public SysSettingsEnum Type { get; set; } = SysSettingsEnum.None;

    [Column("Title")]
    [Sort("Title")]
    public string Title { get; set; } = string.Empty;

    [Column("JsonData")]
    [Sort("JsonData")]
    public string JsonData { get; set; } = string.Empty;

    [Column("Sequence")]
    [Sort("Sequence")]
    public string CurSequence { get; set; } = string.Empty;

    [Column("CreateTime")]
    [Sort("CreateTime")]
    public DateTime CreateTime { get; set; } = DateTimeExtension.Now;
}