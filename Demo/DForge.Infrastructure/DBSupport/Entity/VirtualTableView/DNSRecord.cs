namespace DForge.Infrastructure.DBSupport.Entity.VirtualTableView;

[DefaultSort("ProjectID", SortDirectionEnum.ASC)]
public partial class VTVDNSRecord : IDBEntity, IDBFPrimaryKey<string>
{
    [Column("ID")]
    [Sort("ID")]
    public string PrimaryKey { get; set; } = Unique.GetID();

    [Column("ProjectID")]
    [Sort("ProjectID")]
    public string ProjectID { get; set; } = string.Empty;

    [Column("ProjectName")]
    [Sort("ProjectName")]
    public string ProjectName { get; set; } = string.Empty;

    [Column("DomainID")]
    [Sort("DomainID")]
    public string DomainID { get; set; } = string.Empty;

    [Column("DomainName")]
    [Sort("DomainName")]
    public string DomainName { get; set; } = string.Empty;

    [Column("Type")]
    [Sort("Type")]
    public DNSRecordEnum Type { get; set; }

    [Column("Source")]
    [Sort("Source")]
    public string Source { get; set; } = string.Empty;

    [Column("Target")]
    [Sort("Target")]
    public string Target { get; set; } = string.Empty;

    [Column("TTL")]
    [Sort("TTL")]
    public string TTL { get; set; } = string.Empty;

    [Column("Priority")]
    [Sort("Priority")]
    public string Priority { get; set; } = string.Empty;

    [Column("Proxied")]
    [Sort("Proxied")]
    public bool Proxied { get; set; } = default;

    [Column("IPv4Only")]
    [Sort("IPv4Only")]
    public bool IPv4Only { get; set; } = default;

    [Column("IPv6Only")]
    [Sort("IPv6Only")]
    public bool IPv6Only { get; set; } = default;

    [Column("Remark")]
    [Sort("Remark")]
    public string Remark { get; set; } = string.Empty;

    [Column("Tags")]
    [Sort("Tags")]
    public string Tags { get; set; } = string.Empty;

    [Column("SrcID")]
    [Sort("SrcID")]
    public string SrcID { get; set; } = string.Empty;
}