namespace DForge.Infrastructure.DBSupport.Entity.Table;

[Table("TB_App_Domain")]
[DefaultSort("ID", SortDirectionEnum.ASC)]
public partial class TBAppDomain : DBFStatus, IDBFPrimaryKey<string>, IDomain
{
    [Column("ID")]
    [Sort("ID")]
    public string PrimaryKey { get; set; } = Unique.GetID();

    [Column("ProjectID")]
    [Sort("ProjectID")]
    public string ProjectID { get; set; } = string.Empty;

    [Column("Name")]
    [Sort("Name")]
    public string Name { get; set; } = string.Empty;

    [Column("NameServers")]
    [Sort("NameServers")]
    public string NameServers { get; set; } = string.Empty;

    [Column("RegistChannelID")]
    [Sort("RegistChannelID")]
    public string RegistChannelID { get; set; } = string.Empty;

    [Column("RegistSrcID")]
    [Sort("RegistSrcID")]
    public string RegistSrcID { get; set; } = string.Empty;

    [Column("RegistSrcStatus")]
    [Sort("RegistSrcStatus")]
    public string RegistSrcStatus { get; set; } = string.Empty;

    [Column("AnalyseChannelID")]
    [Sort("AnalyseChannelID")]
    public string AnalyseChannelID { get; set; } = string.Empty;

    [Column("AnalyseSrcID")]
    [Sort("AnalyseSrcID")]
    public string AnalyseSrcID { get; set; } = string.Empty;

    [Column("AnalyseSrcStatus")]
    [Sort("AnalyseSrcStatus")]
    public string AnalyseSrcStatus { get; set; } = string.Empty;

    [Column("AnalyseSrcTrusteeship")]
    [Sort("AnalyseSrcTrusteeship")]
    public string AnalyseSrcTrusteeship { get; set; } = string.Empty;

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