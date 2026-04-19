namespace DForge.Infrastructure.DBSupport.Entity.Table;

[Table("TB_Session_Account")]
public partial class TBSessionAccount : TBSessionBase
{
    [Column("RoleType")]
    [Sort("RoleType")]
    public RoleEnum RoleType { get; set; } = RoleEnum.None;

    [Column("RoleCodes")]
    [Sort("RoleCodes")]
    public string RoleCodes { get; set; } = string.Empty;

    [Column("AccountID")]
    [Sort("AccountID")]
    public string AccountID { get; set; } = string.Empty;
}