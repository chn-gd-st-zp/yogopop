namespace DForge.Infrastructure.DBSupport.Entity.Table;

[Table("TB_Sys_Permission")]
public partial class TBSysPermission : DBTreeNSequence
{
    [Column("Type")]
    [Sort("Type")]
    public PermissionTypeEnum Type { get; set; } = PermissionTypeEnum.None;

    [Column("Name")]
    [Sort("Name")]
    public string Name { get; set; } = string.Empty;

    [Column("AccessLogger")]
    [Sort("AccessLogger")]
    public bool AccessLogger { get; set; } = false;

    [NotMapped]
    public override StatusEnum Status { get; set; } = StatusEnum.None;
}