namespace DForge.Database.EF;

[DIModeForService(DIModeEnum.Exclusive, typeof(IAppProjectRepository))]
public partial class AppProjectRepository : RenewEFDBRepository<TBAppProject, string>, IAppProjectRepository
{
    public async Task<Tuple<List<TBAppProject>, int>> PageAsync(DTOAppProjectPage input)
    {
        var query = DBContext.GetQueryable<TBAppProject>();

        if (input.Name.IsNotEmptyString())
            query = query.Where(o => o.Name.Contains(input.Name));

        if (input.Status.IsNotNullOrDefault())
            query = query.Where(o => o.Status == input.Status);

        var result = await base.PageByQueryableAsync<TBAppProject, DTOSort>(query, input);

        return result;
    }
}