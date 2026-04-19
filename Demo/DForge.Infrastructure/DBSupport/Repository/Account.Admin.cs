namespace DForge.Infrastructure.DBSupport.Repository;

public partial interface IAdminRepository : IDBRepository<TBAccountAdmin, string>, ITransient
{
    public Task<bool> CreateAsync(TBAccountAdmin accountAdmin, TBAccountInfo accountInfo);

    public Task<bool> UpdateAsync(TBAccountAdmin accountAdmin, TBAccountInfo accountInfo);

    public Task<Tuple<List<VTVAccountAdmin>, int>> PageAsync(DTOSysAdminPage input);
}