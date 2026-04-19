namespace DForge.Infrastructure.DBSupport.Repository;

public partial interface IAppProjectRepository : IDBRepository<TBAppProject, string>, ITransient
{
    public Task<Tuple<List<TBAppProject>, int>> PageAsync(DTOAppProjectPage input);
}