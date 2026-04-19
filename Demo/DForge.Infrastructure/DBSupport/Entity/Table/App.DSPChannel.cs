namespace DForge.Infrastructure.DBSupport.Entity.Table;

[Table("TB_App_DSPChannel")]
[DefaultSort("ID", SortDirectionEnum.ASC)]
public partial class TBAppDSPChannel : DBFStatus, IDBFPrimaryKey<string>, IChannel
{
    [Column("ID")]
    [Sort("ID")]
    public string PrimaryKey { get; set; } = Unique.GetID();

    [Column("ProjectID")]
    [Sort("ProjectID")]
    public string ProjectID { get; set; } = string.Empty;

    [Column("DSP")]
    [Sort("DSP")]
    public DSPEnum DSP { get; set; } = DSPEnum.None;

    [Column("Alias")]
    [Sort("Alias")]
    public string Alias { get; set; } = string.Empty;

    [Column("Settings")]
    [Sort("Settings")]
    public string Settings { get; set; } = string.Empty;
}