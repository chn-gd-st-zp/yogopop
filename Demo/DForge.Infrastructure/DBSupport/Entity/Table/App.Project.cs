namespace DForge.Infrastructure.DBSupport.Entity.Table;

[Table("TB_App_Project")]
[DefaultSort("ID", SortDirectionEnum.ASC)]
public partial class TBAppProject : DBFStatus, IDBFPrimaryKey<string>
{
    [Column("ID")]
    [Sort("ID")]
    public string PrimaryKey { get; set; } = Unique.GetID();

    [Column("Name")]
    [Sort("Name")]
    public string Name { get; set; } = string.Empty;
}