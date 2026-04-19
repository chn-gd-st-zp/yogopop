namespace DForge.Infrastructure.DBSupport.Repository;

public partial interface IAppDomainRepository : IDBRepository<TBAppDomain, string>, ITransient
{
    public Task<Tuple<List<VTVDomain>, int>> PageAsync(DTOAppDomainPage input);
}