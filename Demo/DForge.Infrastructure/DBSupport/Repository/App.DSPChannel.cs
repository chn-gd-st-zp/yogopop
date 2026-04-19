namespace DForge.Infrastructure.DBSupport.Repository;

public partial interface IAppDSPChannelRepository : IDBRepository<TBAppDSPChannel, string>, ITransient
{
    public Task<Tuple<List<VTVDSPChannel>, int>> PageAsync(DTOAppDSPChannelPage input);
}