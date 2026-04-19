namespace DForge.Infrastructure.DBSupport.Repository;

public partial interface IAccountInfoRepository : IDBRepository<TBAccountInfo, string>, ITransient
{
    public Task<List<TBAccountInfo>> SingleByAccountAsync(string accountID, string account);

    public TBAccountInfo SingleByRoleAndAccount(RoleEnum[] roleTypes, string account);
}