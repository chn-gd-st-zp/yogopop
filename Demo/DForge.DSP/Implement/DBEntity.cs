namespace DForge.DSP.Implement;

public class Channel : IChannel
{
    public DSPEnum DSP { get; set; }

    public string Alias { get; set; }

    public string Settings { get; set; }
}

public class Domain : IDomain
{
    public string Name { get; set; } = string.Empty;

    public string NameServers { get; set; } = string.Empty;

    public DateTime? CreateTime { get; set; } = default;

    public DateTime? ExpiredTime { get; set; } = default;

    public DMainStatusEnum MainStatus { get; set; } = DMainStatusEnum.None;

    public DSubStatusEnum SubStatus { get; set; } = DSubStatusEnum.None;

    public string RegistSrcID { get; set; } = string.Empty;

    public string AnalyseSrcID { get; set; } = string.Empty;

    public string RegistSrcStatus { get; set; } = string.Empty;

    public string AnalyseSrcStatus { get; set; } = string.Empty;

    public string AnalyseSrcTrusteeship { get; set; } = string.Empty;
}

public class DNSRecord : IDNSRecord
{
    public DNSRecordEnum Type { get; set; }

    public string Source { get; set; } = string.Empty;

    public string Target { get; set; } = string.Empty;

    public string TTL { get; set; } = string.Empty;

    public string Remark { get; set; } = string.Empty;

    public string Tags { get; set; } = string.Empty;

    public string Priority { get; set; } = string.Empty;

    public bool Proxied { get; set; } = default;

    public bool IPv4Only { get; set; } = default;

    public bool IPv6Only { get; set; } = default;

    public string SrcID { get; set; } = string.Empty;
}