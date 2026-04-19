namespace DForge.Infrastructure.DBSupport.Base;

public class DBFStatus : IDBEntity, IDBFStatus<StatusEnum>
{
    [Column("Status")]
    [Sort("Status")]
    public virtual StatusEnum Status { get; set; } = StatusEnum.Normal;
}