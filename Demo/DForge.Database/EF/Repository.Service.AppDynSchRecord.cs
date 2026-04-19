namespace DForge.Database.EF;

[DIModeForService(DIModeEnum.Exclusive, typeof(IAppDynSchRecordRepository))]
public partial class AppDynSchRecordRepository : RenewEFDBRepository<TBAppDynSchRecord, long>, IAppDynSchRecordRepository
{
    public async Task<Tuple<List<TBAppDynSchRecord>, int>> PageAsync(DTOAppDynSchRecordPage input)
    {
        var query = DBContext.GetQueryable<TBAppDynSchRecord>()
            .Where(o => input.DynSchs.Contains(o.MainType) && o.TriggerID == input.PrimaryKey);

        var result = await base.PageByQueryableAsync<TBAppDynSchRecord, DTOSort>(query, input);

        return result;
    }
}