namespace DForge.Infrastructure.DBSupport.Repository;

public partial interface IAppDNSRecordRepository : IDBRepository<TBAppDNSRecord, string>, ITransient
{
    public Task<List<VTVDNSRecord>> ListAsync(DTOAppDNSRecordList input);
}