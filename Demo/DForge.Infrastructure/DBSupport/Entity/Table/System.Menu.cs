namespace DForge.Infrastructure.DBSupport.Entity.Table;

[Table("TB_Sys_Menu")]
[DefaultSort("Type", SortDirectionEnum.ASC)]
public partial class TBSysMenu : DBTreeNSequence
{
    [Column("Category")]
    [Sort("Category")]
    public SysCategoryEnum Category { get; set; } = SysCategoryEnum.None;

    [Column("Type")]
    [Sort("Type")]
    public SysMenuEnum Type { get; set; } = SysMenuEnum.None;

    [Column("Name")]
    [Sort("Name")]
    public override string Name { get; set; } = string.Empty;

    [Column("Route")]
    [Sort("Route")]
    public string Route { get; set; } = string.Empty;

    [Column("PermissionCode")]
    [Sort("PermissionCode")]
    public string PermissionCode { get; set; } = string.Empty;
}