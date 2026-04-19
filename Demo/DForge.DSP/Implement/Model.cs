namespace DForge.DSP.Implement;

public class DSResult<TData> : IDSResult<TData>
{
    public string ErrorMsg { get; set; } = string.Empty;

    public Exception ErrorObj { get; set; } = default;

    public TData Data { get; set; } = default;

    public DSResult<TData> Populate(string errorMsg, Exception errorObj = null)
    {
        ErrorMsg = errorMsg;
        ErrorObj = errorObj;
        return this;
    }
}

[DIModeForService(DIModeEnum.Exclusive, typeof(IDSResult<,>))]
public class DSResult<TTrigger, TData> : DSResult<TData>, IDSResult<TTrigger, TData>, ITransient
{
    public TTrigger Trigger { get; set; } = default;

    public bool IsSuccess { get; set; } = default;

    public DSResult<TTrigger, TData> Populate(TTrigger trigger, string errorMsg, Exception errorObj = null)
    {
        Trigger = trigger;
        ErrorMsg = errorMsg;
        ErrorObj = errorObj;
        return this;
    }
}

public class DSDetails<TDomain, TDNSRecord> : IDSDetails<TDomain, TDNSRecord>
    where TDomain : class, IDomain, new()
    where TDNSRecord : class, IDNSRecord, new()
{
    public TDomain Domain { get; set; } = default;

    public IEnumerable<TDNSRecord> DNSRecords { get; set; } = default;
}