namespace DForge.DSP;

public class DSPDynSchMQMsgExtraData : IDynSchMQMsgExtraData
{
    public IChannel Channel { get; set; }

    public IEnumerable<IDomain> Domains { get; set; }

    public IEnumerable<string> NameServers { get; set; }

    public IEnumerable<IDNSRecord> DNSRecords { get; set; }
}