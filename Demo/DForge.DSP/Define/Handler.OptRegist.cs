namespace DForge.DSP.Define;

public interface IDSOptRegistSetting : IDSSettings
{
    public string DomainNSModifyRoute { get; }
    public string DomainNSsModifyRoute { get; }
}

public interface IDSOptRegistHandler<TDomain, TDNSRecord> : IDSHandler<TDomain, TDNSRecord>
    where TDomain : class, IDomain, new()
    where TDNSRecord : class, IDNSRecord, new()
{
    public Task<IDSResult<TDomain, bool>> NSModify(TDomain domain, params string[] nsArray);

    public Task<IDSResult<int>> NSModify(IEnumerable<TDomain> domains, params string[] nsArray);
}