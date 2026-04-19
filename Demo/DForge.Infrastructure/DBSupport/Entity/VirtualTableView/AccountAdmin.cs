namespace DForge.Infrastructure.DBSupport.Entity.VirtualTableView;

[DefaultSort("CreateTime", SortDirectionEnum.DESC)]
public class VTVAccountAdmin : DBFStatus
{
    [Column("ID")]
    [Sort("ID")]
    public string PrimaryKey { get; set; }

    [Column("RoleType")]
    [Sort("RoleType")]
    public RoleEnum RoleType { get; set; }

    [Column("RoleID")]
    [Sort("RoleID")]
    public string RoleID { get; set; }

    [Column("RoleName")]
    [Sort("RoleName")]
    public string RoleName { get; set; }

    [Column("UserName")]
    [Sort("UserName")]
    public string UserName { get; set; }

    [Column("MFASecret")]
    [Sort("MFASecret")]
    public string MFASecret { get; set; } = string.Empty;

    [Column("CreateTime")]
    [Sort("CreateTime")]
    public DateTime CreateTime { get; set; }
}