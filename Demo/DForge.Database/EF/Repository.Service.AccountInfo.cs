namespace DForge.Database.EF;

[DIModeForService(DIModeEnum.Exclusive, typeof(IAccountInfoRepository))]
public partial class AccountInfoRepository : RenewEFDBRepository<TBAccountInfo, string>, IAccountInfoRepository
{
    public async Task<List<TBAccountInfo>> SingleByAccountAsync(string accountID, string account)
    {
        var query_ai = DBContext.GetQueryable<TBAccountInfo>()
            .Where(o => o.PrimaryKey != accountID)
            .Where(o => o.RoleType == RoleEnum.User)
            .Where(o => o.UserName == account || o.Mobile == account || o.Email == account);

        var query = query_ai;

        var result = await query.ToListAsync();

        return result;
    }

    public TBAccountInfo SingleByRoleAndAccount(RoleEnum[] roleTypes, string account)
    {
        var query = DBContext.GetQueryable<TBAccountInfo>();

        query = query.Where(o => roleTypes.Contains(o.RoleType));

        if (AccountHelper.IsUserName(account))
            query = query.Where(o => o.UserName == account);
        else if (AccountHelper.IsMobile(account))
            query = query.Where(o => o.Prefix + o.Mobile == account);
        else if (AccountHelper.IsEmail(account))
            query = query.Where(o => o.Email == account);

        var result = query.SingleOrDefault();

        return result;
    }
}