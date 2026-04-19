namespace DForge.Infrastructure.DBSupport.Repository;

public partial interface ISessionRepository : IDBRepository, ITransient
{
    public Task<bool> CreateAsync(TBSessionDevice sd);

    public Task<bool> CreateAsync(TBSessionDevice sd, TBSessionAccount sa);

    public Task<bool> UpdateAsync(string accessToken, DateTime expiredTime, DateTime? updateTime = null);

    public Task<Tuple<List<VTVSessionRecord>, int>> PageAsync(DTOSessionPage input);
}