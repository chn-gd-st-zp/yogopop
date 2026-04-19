namespace DForge.Infrastructure.DBSupport.Entity.Table;

[Table("TB_Sys_AccessRecord")]
[DefaultSort("CreateTime", SortDirectionEnum.DESC)]
public partial class TBSysAccessRecord : IDBEntity, IDBFPrimaryKey<string>
{
    [Column("ID")]
    [Sort("ID")]
    public string PrimaryKey { get; set; } = Unique.GetID();

    [Column("GroupID")]
    [Sort("GroupID")]
    public string GroupID { get; set; } = string.Empty;

    [Column("RoleType")]
    [Sort("RoleType")]
    public RoleEnum RoleType { get; set; } = RoleEnum.None;

    [Column("AccountID")]
    [Sort("AccountID")]
    public string AccountID { get; set; } = string.Empty;

    [Column("UserName")]
    [Sort("UserName")]
    public string UserName { get; set; } = string.Empty;

    [Column("OperationType")]
    [Sort("OperationType")]
    public OperationTypeEnum OperationType { get; set; } = OperationTypeEnum.None;

    [Column("Action")]
    [Sort("Action")]
    public string Action { get; set; } = string.Empty;

    [Column("TBName")]
    [Sort("TBName")]
    public string TBName { get; set; } = string.Empty;

    [Column("TBValue")]
    [Sort("TBValue")]
    public string TBValue { get; set; } = string.Empty;

    [Column("PKName")]
    [Sort("PKName")]
    public string PKName { get; set; } = string.Empty;

    [Column("PKValue")]
    [Sort("PKValue")]
    public string PKValue { get; set; } = string.Empty;

    [Column("TriggerName")]
    [Sort("TriggerName")]
    public string TriggerName { get; set; } = string.Empty;

    [Column("Content")]
    [Sort("Content")]
    public string Content { get; set; } = string.Empty;

    [Column("ExecResult")]
    [Sort("ExecResult")]
    public string ExecResult { get; set; } = string.Empty;

    [Column("CreateTime")]
    [Sort("CreateTime")]
    public DateTime CreateTime { get; set; } = DateTimeExtension.Now;
}