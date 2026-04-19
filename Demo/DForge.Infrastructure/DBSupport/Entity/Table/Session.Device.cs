namespace DForge.Infrastructure.DBSupport.Entity.Table;

[Table("TB_Session_Device")]
public partial class TBSessionDevice : TBSessionBase
{
    [Column("PushToken")]
    [Sort("PushToken")]
    public string PushToken { get; set; } = string.Empty;

    [Column("Entry")]
    [Sort("Entry")]
    public EntryEnum Entry { get; set; } = EntryEnum.None;

    [Column("IP")]
    [Sort("IP")]
    public string IP { get; set; } = string.Empty;
}