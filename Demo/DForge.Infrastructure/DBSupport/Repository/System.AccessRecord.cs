namespace DForge.Infrastructure.DBSupport.Repository;

public partial interface ISysAccessRecordRepository : IDBRepository<TBSysAccessRecord, string>, ITransient
{
    public Task<Tuple<List<TBSysAccessRecord>, int>> PageAsync(DTOSysAccessRecordPage input, string groupID = "", params RoleEnum[] roles);
}