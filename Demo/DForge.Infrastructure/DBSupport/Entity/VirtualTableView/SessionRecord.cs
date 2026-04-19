namespace DForge.Infrastructure.DBSupport.Entity.VirtualTableView;

[DefaultSort("CreateTime", SortDirectionEnum.DESC)]
public class VTVSessionRecord : IDBEntity
{
    [Column("AccessToken")]
    [Sort("AccessToken")]
    public string AccessToken { get; set; }

    [Column("PushToken")]
    [Sort("PushToken")]
    public string PushToken { get; set; }

    [Column("Entry")]
    [Sort("Entry")]
    public EntryEnum Entry { get; set; }

    [Column("IP")]
    [Sort("IP")]
    public string IP { get; set; }

    [Column("RoleType")]
    [Sort("RoleType")]
    public string RoleType { get; set; }

    [Column("UserName")]
    [Sort("UserName")]
    public string UserName { get; set; }

    [Column("Mobile")]
    [Sort("Mobile")]
    [PropertyPermission(GlobalPermissionEnum.Account_Property_Search_Mobile, GlobalPermissionEnum.Account_Property_Search, PermissionPropertyFailHandleEnum.Mosaic)]
    public string Mobile { get; set; }

    [Column("Email")]
    [Sort("Email")]
    [PropertyPermission(GlobalPermissionEnum.Account_Property_Search_Email, GlobalPermissionEnum.Account_Property_Search, PermissionPropertyFailHandleEnum.Mosaic)]
    public string Email { get; set; }

    [Column("CreateTime")]
    [Sort("CreateTime")]
    public DateTime CreateTime { get; set; }

    [Column("UpdateTime")]
    [Sort("UpdateTime")]
    public DateTime UpdateTime { get; set; }

    [Column("ExpiredTime")]
    [Sort("ExpiredTime")]
    public DateTime ExpiredTime { get; set; }
}