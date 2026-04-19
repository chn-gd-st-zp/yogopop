namespace DForge.Infrastructure.DBSupport.Entity.Table;

[Table("TB_Sys_RolePermission")]
[DefaultSort("ID", SortDirectionEnum.ASC)]
public partial class TBSysRolePermission : IDBEntity, IDBFPrimaryKey<string>
{
    [Column("ID")]
    [Sort("ID")]
    public string PrimaryKey { get; set; } = Unique.GetID();

    [Column("RoleCode")]
    [Sort("RoleCode")]
    public string RoleCode { get; set; } = string.Empty;

    [Column("PermissionCode")]
    [Sort("PermissionCode")]
    public string PermissionCode { get; set; } = string.Empty;
}