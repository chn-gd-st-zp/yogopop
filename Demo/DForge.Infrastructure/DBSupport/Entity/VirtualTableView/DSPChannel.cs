namespace DForge.Infrastructure.DBSupport.Entity.VirtualTableView;

[DefaultSort("ID", SortDirectionEnum.ASC)]
public class VTVDSPChannel : DBFStatus
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

    [Column("DSP")]
    [Sort("DSP")]
    public DSPEnum DSP { get; set; } = DSPEnum.None;

    [Column("Alias")]
    [Sort("Alias")]
    public string Alias { get; set; } = string.Empty;

    [Column("Alias")]
    [Sort("Alias")]
    public string Settings { get; set; } = string.Empty;
}