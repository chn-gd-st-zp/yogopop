namespace DForge.DSP.Define;

public interface IChannel
{
    public DSPEnum DSP { get; set; }

    public string Alias { get; set; }

    public string Settings { get; set; }
}

public interface IDomain
{
    public string Name { get; set; }

    public string NameServers { get; set; }

    public DateTime? CreateTime { get; set; }

    public DateTime? ExpiredTime { get; set; }

    public DMainStatusEnum MainStatus { get; set; }

    public DSubStatusEnum SubStatus { get; set; }

    public string RegistSrcID { get; set; }

    public string RegistSrcStatus { get; set; }

    public string AnalyseSrcID { get; set; }

    public string AnalyseSrcStatus { get; set; }

    public string AnalyseSrcTrusteeship { get; set; }
}

public interface IDNSRecord
{
    public DNSRecordEnum Type { get; set; }

    public string Source { get; set; }

    public string Target { get; set; }

    public string TTL { get; set; }

    public string Priority { get; set; }

    public bool Proxied { get; set; }

    public bool IPv4Only { get; set; }

    public bool IPv6Only { get; set; }

    public string Remark { get; set; }

    public string Tags { get; set; }

    public string SrcID { get; set; }
}