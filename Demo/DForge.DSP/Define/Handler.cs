namespace DForge.DSP.Define;

public interface IDSSettings : ITransient
{
    public string BaseUrl { get; }

    public string DomainQueryRoute { get; }
    public string DomainsQueryRoute { get; }
}

public interface IDSHandler<TDomain, TDNSRecord> : ITransient
    where TDomain : class, IDomain, new()
    where TDNSRecord : class, IDNSRecord, new()
{
    public Tuple<DMainStatusEnum, DSubStatusEnum> StatusExchange(object data);

    public DNSRecordEnum DNSRecordExchange(object data);

    public Task<IDSResult<IDSDetails<TDomain, TDNSRecord>>> DomainQuery(DSOptEnum dbOpt, string domainKey);

    public Task<IDSResult<IEnumerable<IDSDetails<TDomain, TDNSRecord>>>> DomainQuery(DSOptEnum dbOpt);
}