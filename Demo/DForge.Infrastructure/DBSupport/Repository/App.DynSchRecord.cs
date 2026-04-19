namespace DForge.Infrastructure.DBSupport.Repository;

public partial interface IAppDynSchRecordRepository : IDBRepository<TBAppDynSchRecord, long>, ITransient
{
    public Task<Tuple<List<TBAppDynSchRecord>, int>> PageAsync(DTOAppDynSchRecordPage input);
}