
namespace DForge.Database.EF;

[DIModeForService(DIModeEnum.Exclusive, typeof(ISysAccessRecordRepository))]
public partial class SysAccessRecordRepository : RenewEFDBRepository<TBSysAccessRecord, string>, ISysAccessRecordRepository
{
    public async Task<Tuple<List<TBSysAccessRecord>, int>> PageAsync(DTOSysAccessRecordPage input, string groupID = "", params RoleEnum[] roles)
    {
        var query = DBContext.GetQueryable<TBSysAccessRecord>().Where(o => o.GroupID == groupID);

        if (roles.IsNotEmpty())
            query = query.Where(o => roles.Contains(o.RoleType));

        if (input.Keyword.IsNotEmptyString())
            query = query.Where(o => o.Content.Contains(input.Keyword));

        if (input.AccountID.IsNotEmptyString())
            query = query.Where(o => o.AccountID == input.AccountID);

        if (input.UserName.IsNotEmptyString())
            query = query.Where(o => o.UserName.Contains(input.UserName));

        if (input.OperationType.IsNotNullOrDefault())
            query = query.Where(o => o.OperationType == input.OperationType);

        if (input.TBName.IsNotEmptyString() && input.TBName.ToLower() != "None".ToLower())
            query = query.Where(o => o.TBName.ToLower() == input.TBName.ToLower());

        if (input.PKValue.IsNotEmptyString())
            query = query.Where(o => o.PKValue == input.PKValue);

        if (input.CreateTimeRange != null)
        {
            if (input.CreateTimeRange.Begin.HasValue && input.CreateTimeRange.End.HasValue)
                query = query.Where(o => input.CreateTimeRange.Begin <= o.CreateTime && o.CreateTime <= input.CreateTimeRange.End);
            else if (input.CreateTimeRange.Begin.HasValue)
                query = query.Where(o => input.CreateTimeRange.Begin <= o.CreateTime);
            else if (input.CreateTimeRange.End.HasValue)
                query = query.Where(o => o.CreateTime <= input.CreateTimeRange.End);
        }

        var result = await base.PageByQueryableAsync<TBSysAccessRecord, DTOSort>(query, input);

        return result;
    }
}