namespace DForge.DSP.Define;

public interface IDSResult<TData>
{
    public string ErrorMsg { get; set; }

    public Exception ErrorObj { get; set; }

    public TData Data { get; set; }
}

public interface IDSResult<TTrigger, TData> : IDSResult<TData>, ICuncurrencyTaskResult<TTrigger, TData>
{
    public new string ErrorMsg { get; set; }

    public new Exception ErrorObj { get; set; }

    public new TData Data { get; set; }
}

public interface IDSDetails<TDomain, TDNSRecord>
    where TDomain : class, IDomain, new()
    where TDNSRecord : class, IDNSRecord, new()
{
    public TDomain Domain { get; set; }

    public IEnumerable<TDNSRecord> DNSRecords { get; set; }
}