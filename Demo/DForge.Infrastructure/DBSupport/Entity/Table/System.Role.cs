namespace DForge.Infrastructure.DBSupport.Entity.Table;

[Table("TB_Sys_Role")]
[DefaultSort("Type", SortDirectionEnum.ASC)]
public partial class TBSysRole : DBTree
{
    [Column("Type")]
    [Sort("Type")]
    public RoleEnum Type { get; set; } = RoleEnum.None;

    [Column("SubType")]
    [Sort("SubType")]
    public UserTypeEnum SubType { get; set; } = UserTypeEnum.None;

    [Column("Name")]
    [Sort("Name")]
    public string Name { get; set; } = string.Empty;

    [Column("Level")]
    [Sort("Level")]
    public int Level { get; set; } = 0;

    [Column("GroupID")]
    [Sort("GroupID")]
    public string GroupID { get; set; } = string.Empty;

    [Column("CreateTime")]
    [Sort("CreateTime")]
    public DateTime CreateTime { get; set; } = DateTimeExtension.Now;
}