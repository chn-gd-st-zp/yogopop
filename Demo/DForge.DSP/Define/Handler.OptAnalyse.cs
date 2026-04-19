namespace DForge.DSP.Define;

public interface IDSOptAnalyseSetting : IDSSettings
{
    public string DomainAddRoute { get; }
    public string DomainsAddRoute { get; }

    public string DomainDelRoute { get; }
    public string DomainsDelRoute { get; }

    public string DomainEdtRoute { get; }
    public string DomainsEdtRoute { get; }

    public string DNSRecordQueryRoute { get; }
    public string DNSRecordsQueryRoute { get; }

    public string DNSRecordAddRoute { get; }

    public string DNSRecordDelRoute { get; }

    public string DNSRecordEdtRoute { get; }
}

public interface IDSOptAnalyseHandler<TDomain, TDNSRecord> : IDSHandler<TDomain, TDNSRecord>
    where TDomain : class, IDomain, new()
    where TDNSRecord : class, IDNSRecord, new()
{
    public Task<IDSResult<string, TDomain>> DomainAdd(string domain, string trusteeship);

    public Task<IDSResult<IEnumerable<TDomain>>> DomainAdd(IEnumerable<string> domains, string trusteeship);

    public Task<IDSResult<TDomain, bool>> DomainDel(TDomain domain);

    public Task<IDSResult<int>> DomainDel(IEnumerable<TDomain> domains);

    public Task<IDSResult<TDomain, bool>> DomainEdt(TDomain domain);

    public Task<IDSResult<int>> DomainEdt(IEnumerable<TDomain> domains);

    public Task<IDSResult<TDNSRecord>> DNSRecordQuery(TDomain domain, string dnsKey);

    public Task<IDSResult<IEnumerable<TDNSRecord>>> DNSRecordQuery(TDomain domain);

    public Task<IDSResult<int>> DNSRecordAdd(TDomain domain, params TDNSRecord[] dnsRecordArray);

    public Task<IDSResult<int>> DNSRecordDel(TDomain domain, params TDNSRecord[] dnsRecordArray);

    public Task<IDSResult<int>> DNSRecordEdt(TDomain domain, params TDNSRecord[] dnsRecordArray);
}