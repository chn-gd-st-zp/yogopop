namespace DForge.Infrastructure.DBSupport.Base;

[DefaultSort("AccessToken", SortDirectionEnum.ASC)]
public abstract class TBSessionBase : IDBEntity, IDBFPrimaryKey<string>
{
    [Column("AccessToken")]
    [Sort("AccessToken")]
    public string PrimaryKey { get; set; } = Unique.GetID();

    [Column("CreateTime")]
    [Sort("CreateTime")]
    public DateTime CreateTime { get; set; } = DateTimeExtension.Now;

    [Column("UpdateTime")]
    [Sort("UpdateTime")]
    public DateTime UpdateTime { get; set; } = DateTimeExtension.Now;

    [Column("ExpiredTime")]
    [Sort("ExpiredTime")]
    public DateTime ExpiredTime { get; set; } = DateTimeExtension.Now;
}