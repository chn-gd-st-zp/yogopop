namespace DForge.DSP.Implement;

public abstract class DSHandler<TSettings, TDomain, TDNSRecord> : IDSHandler<TDomain, TDNSRecord>
    where TSettings : class, IDSSettings
    where TDomain : class, IDomain, new()
    where TDNSRecord : class, IDNSRecord, new()
{
    protected TSettings Settings { get; private set; }

    //public DSHandler() { }

    public DSHandler(TSettings settings) { Settings = settings; }

    public abstract Tuple<DMainStatusEnum, DSubStatusEnum> StatusExchange(object data);

    public virtual DNSRecordEnum DNSRecordExchange(object data)
    {
        if (data == null) return DNSRecordEnum.None;

        var dataStr = data.ToString();
        if (dataStr.IsEmptyString()) return DNSRecordEnum.None;
        return dataStr.ToEnum(DNSRecordEnum.None);
    }

    public abstract Task<IDSResult<IDSDetails<TDomain, TDNSRecord>>> DomainQuery(DSOptEnum dbOpt, string domainKey);

    public abstract Task<IDSResult<IEnumerable<IDSDetails<TDomain, TDNSRecord>>>> DomainQuery(DSOptEnum dbOpt);
}

public abstract class DSOptRegistHandler<TSettings, TDomain, TDNSRecord> : DSHandler<TSettings, TDomain, TDNSRecord>, IDSOptRegistHandler<TDomain, TDNSRecord>
    where TSettings : class, IDSOptRegistSetting
    where TDomain : class, IDomain, new()
    where TDNSRecord : class, IDNSRecord, new()
{
    //public DSOptRegistHandler() : base() { }

    public DSOptRegistHandler(TSettings settings) : base(settings) { }

    public abstract Task<IDSResult<TDomain, bool>> NSModify(TDomain domain, params string[] nsArray);

    public abstract Task<IDSResult<int>> NSModify(IEnumerable<TDomain> domains, params string[] nsArray);
}

public abstract class DSOptAnalyseHandler<TSettings, TDomain, TDNSRecord> : DSHandler<TSettings, TDomain, TDNSRecord>, IDSOptAnalyseHandler<TDomain, TDNSRecord>
    where TSettings : class, IDSOptAnalyseSetting
    where TDomain : class, IDomain, new()
    where TDNSRecord : class, IDNSRecord, new()
{
    //public DSOptAnalyseHandler() : base() { }

    public DSOptAnalyseHandler(TSettings settings) : base(settings) { }

    public abstract Task<IDSResult<string, TDomain>> DomainAdd(string domain, string trusteeship);

    public abstract Task<IDSResult<IEnumerable<TDomain>>> DomainAdd(IEnumerable<string> domains, string trusteeship);

    public abstract Task<IDSResult<TDomain, bool>> DomainDel(TDomain domain);

    public abstract Task<IDSResult<int>> DomainDel(IEnumerable<TDomain> domains);

    public abstract Task<IDSResult<TDomain, bool>> DomainEdt(TDomain domain);

    public abstract Task<IDSResult<int>> DomainEdt(IEnumerable<TDomain> domains);

    public abstract Task<IDSResult<TDNSRecord>> DNSRecordQuery(TDomain domain, string dnsKey);

    public abstract Task<IDSResult<IEnumerable<TDNSRecord>>> DNSRecordQuery(TDomain domain);

    public abstract Task<IDSResult<int>> DNSRecordAdd(TDomain domain, params TDNSRecord[] dnsRecordArray);

    public abstract Task<IDSResult<int>> DNSRecordDel(TDomain domain, params TDNSRecord[] dnsRecordArray);

    public abstract Task<IDSResult<int>> DNSRecordEdt(TDomain domain, params TDNSRecord[] dnsRecordArray);
}