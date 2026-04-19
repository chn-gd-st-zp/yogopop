namespace DForge.DSP.Define;

public interface IDSPRepository : IDBRepository, ITransient
{
    public Task<IEnumerable<IDNSRecord>> DNSRecords(IDomain domain);

    public Task SaveAsync<TLoggerTrigger>(IYogoLogger<TLoggerTrigger> logger, DynSchMQMsg msg, DSOptEnum dsOpt, IChannel channel, IEnumerable<IDomain> domains, IEnumerable<IDNSRecord> dnsRecords)
        where TLoggerTrigger : class;

    public Task SaveAsync<TLoggerTrigger>(IYogoLogger<TLoggerTrigger> logger, DynSchMQMsg msg, DSOptEnum dsOpt, IChannel channel, IEnumerable<IDomain> domains)
        where TLoggerTrigger : class;

    public Task SaveAsync<TLoggerTrigger>(IYogoLogger<TLoggerTrigger> logger, DynSchMQMsg msg, DSOptEnum dsOpt, IChannel channel, IDomain domain,
        IEnumerable<IDNSRecord> dnsRecords_A, IEnumerable<IDNSRecord> dnsRecords_D, IEnumerable<IDNSRecord> dnsRecords_E)
        where TLoggerTrigger : class;
}