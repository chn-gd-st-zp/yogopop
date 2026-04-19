namespace DForge.Infrastructure.DBSupport.Entity.VirtualTableView;

[DefaultSort("ProjectID", SortDirectionEnum.ASC)]
public partial class VTVDomain : DBFStatus, IDBFPrimaryKey<string>
{
    [Column("ID")]
    [Sort("ID")]
    public string PrimaryKey { get; set; } = Unique.GetID();

    [Column("ProjectID")]
    [Sort("ProjectID")]
    public string ProjectID { get; set; } = string.Empty;

    [Column("ProjectName")]
    [Sort("ProjectName")]
    public string ProjectName { get; set; } = string.Empty;

    [Column("RegistChannelID")]
    [Sort("RegistChannelID")]
    public string RegistChannelID { get; set; } = string.Empty;

    [Column("RegistChannelAlias")]
    [Sort("RegistChannelAlias")]
    public string RegistChannelAlias { get; set; } = string.Empty;

    [Column("AnalyseChannelID")]
    [Sort("AnalyseChannelID")]
    public string AnalyseChannelID { get; set; } = string.Empty;

    [Column("AnalyseChannelAlias")]
    [Sort("AnalyseChannelAlias")]
    public string AnalyseChannelAlias { get; set; } = string.Empty;

    [Column("Name")]
    [Sort("Name")]
    public string Name { get; set; } = string.Empty;

    [Column("NameServers")]
    [Sort("NameServices")]
    public string NameServers { get; set; } = string.Empty;

    [Column("CreateTime")]
    [Sort("CreateTime")]
    public DateTime? CreateTime { get; set; } = default;

    [Column("ExpiredTime")]
    [Sort("ExpiredTime")]
    public DateTime? ExpiredTime { get; set; } = default;

    [Column("MainStatus")]
    [Sort("MainStatus")]
    public DMainStatusEnum MainStatus { get; set; } = DMainStatusEnum.None;

    [Column("SubStatus")]
    [Sort("SubStatus")]
    public DSubStatusEnum SubStatus { get; set; } = DSubStatusEnum.None;
}