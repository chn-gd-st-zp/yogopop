namespace DForge.Infrastructure.DBSupport.Base;

[DefaultSort("ID", SortDirectionEnum.ASC)]
public abstract class TBAccountBase : DBFStatus, IDBFPrimaryKey<string>
{
    [Column("ID")]
    [Sort("ID")]
    public string PrimaryKey { get; set; } = Unique.GetID();

    [Column("RoleID")]
    [Sort("RoleID")]
    public string RoleID { get; set; } = string.Empty;

    [Column("Secret")]
    [Sort("Secret")]
    public string Secret { get; set; } = string.Empty;

    [Column("Password")]
    [Sort("Password")]
    public string Password { get; set; } = string.Empty;

    [Column("UserName")]
    [Sort("UserName")]
    public string UserName { get; set; } = string.Empty;

    [Column("CreateTime")]
    [Sort("CreateTime")]
    public DateTime CreateTime { get; set; } = DateTimeExtension.Now;

    [Column("UpdateTime")]
    [Sort("UpdateTime")]
    public DateTime UpdateTime { get; set; } = DateTimeExtension.Now;
}