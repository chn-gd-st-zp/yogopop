namespace DForge.Database.EF;

[DIModeForService(DIModeEnum.Exclusive, typeof(ISessionRepository))]
public partial class SessionRepository : RenewEFDBRepository<TBSessionAccount, string>, ISessionRepository
{
    public async Task<bool> CreateAsync(TBSessionDevice sd)
    {
        bool result = true;

        result = await DBContext.CreateAsync(sd);

        return result;
    }

    public async Task<bool> CreateAsync(TBSessionDevice sd, TBSessionAccount sa)
    {
        bool result = false;

        using (var tranScope = UnitOfWork.GenerateTransactionScope())
        {
            if (sd != null)
            {
                //var sessionDevice = DBContext.Single<TBSessionDevice>(o => o.PushToken == sd.PushToken);
                //result = sessionDevice == null ? DBContext.Create(sd, false) : DBContext.Update(sd, false);
                result = await DBContext.CreateAsync(sd, false);
                if (!result)
                    return result;
            }

            if (sa != null)
            {
                result = await DBContext.CreateAsync(sa, false);
                if (!result)
                    return result;
            }

            if (result)
            {
                DBContext.SaveChanges();
                tranScope.Complete();
            }
        }

        return result;
    }

    public async Task<bool> UpdateAsync(string accessToken, DateTime expiredTime, DateTime? updateTime = null)
    {
        bool result = false;

        var sd = await DBContext.SingleAsync<TBSessionDevice>(o => o.PrimaryKey == accessToken);
        var sa = await DBContext.SingleAsync<TBSessionAccount>(o => o.PrimaryKey == accessToken);

        if (sd != null)
        {
            sd.UpdateTime = updateTime.HasValue ? updateTime.Value : sd.UpdateTime;
            sd.ExpiredTime = expiredTime;
        }

        if (sa != null)
        {
            sa.UpdateTime = updateTime.HasValue ? updateTime.Value : sa.UpdateTime;
            sa.ExpiredTime = expiredTime;
        }

        using (var tranScope = UnitOfWork.GenerateTransactionScope())
        {
            if (sd != null)
            {
                result = DBContext.Update(sd, false);
                if (!result)
                    return result;
            }

            if (result && sa != null)
            {
                result = DBContext.Update(sa, false);
                if (!result)
                    return result;
            }

            if (result)
            {
                DBContext.SaveChanges();
                tranScope.Complete();
            }
        }

        return result;
    }

    public async Task<Tuple<List<VTVSessionRecord>, int>> PageAsync(DTOSessionPage input)
    {
        var query_sa = DBContext.GetQueryable<TBSessionAccount>();
        var query_sd = DBContext.GetQueryable<TBSessionDevice>();
        var query_ai = DBContext.GetQueryable<TBAccountInfo>();

        if (input.RoleType.IsNotNullOrDefault())
            query_sa = query_sa.Where(o => o.RoleType == input.RoleType);

        if (input.Entry.IsNotNullOrDefault())
            query_sd = query_sd.Where(o => o.Entry == input.Entry);

        if (!input.AccessToken.IsEmptyString())
        {
            query_sa = query_sa.Where(o => o.PrimaryKey == input.AccessToken);
            query_sd = query_sd.Where(o => o.PrimaryKey == input.AccessToken);
        }

        if (!input.Account.IsEmptyString())
            query_ai = query_ai.Where(o => o.UserName.Contains(input.Account) || o.Email.Contains(input.Account) || (o.Prefix + o.Mobile).Contains(input.Account));

        if (input.CreateTimeRange != null)
        {
            if (input.CreateTimeRange.Begin.HasValue && input.CreateTimeRange.End.HasValue)
                query_sa = query_sa.Where(o => input.CreateTimeRange.Begin <= o.CreateTime && o.CreateTime <= input.CreateTimeRange.End);
            else if (input.CreateTimeRange.Begin.HasValue)
                query_sa = query_sa.Where(o => input.CreateTimeRange.Begin <= o.CreateTime);
            else if (input.CreateTimeRange.End.HasValue)
                query_sa = query_sa.Where(o => o.CreateTime <= input.CreateTimeRange.End);
        }

        if (input.UpdateTimeRange != null)
        {
            if (input.UpdateTimeRange.Begin.HasValue && input.UpdateTimeRange.End.HasValue)
                query_sa = query_sa.Where(o => input.UpdateTimeRange.Begin <= o.UpdateTime && o.UpdateTime <= input.UpdateTimeRange.End);
            else if (input.UpdateTimeRange.Begin.HasValue)
                query_sa = query_sa.Where(o => input.UpdateTimeRange.Begin <= o.UpdateTime);
            else if (input.UpdateTimeRange.End.HasValue)
                query_sa = query_sa.Where(o => o.UpdateTime <= input.UpdateTimeRange.End);
        }

        if (input.ExpiredTimeRange != null)
        {
            if (input.ExpiredTimeRange.Begin.HasValue && input.ExpiredTimeRange.End.HasValue)
                query_sa = query_sa.Where(o => input.ExpiredTimeRange.Begin <= o.ExpiredTime && o.ExpiredTime <= input.ExpiredTimeRange.End);
            else if (input.ExpiredTimeRange.Begin.HasValue)
                query_sa = query_sa.Where(o => input.ExpiredTimeRange.Begin <= o.ExpiredTime);
            else if (input.ExpiredTimeRange.End.HasValue)
                query_sa = query_sa.Where(o => o.ExpiredTime <= input.ExpiredTimeRange.End);
        }

        var query = query_sa
            .GroupJoin(query_sd, l => l.PrimaryKey, r => r.PrimaryKey, (l, r) => new { sa = l, r })
            .SelectMany(o => o.r.DefaultIfEmpty(), (l, r) => new { l.sa, sd = r })
            .GroupJoin(query_ai, l => l.sa.AccountID, r => r.PrimaryKey, (l, r) => new { l.sa, l.sd, r })
            .SelectMany(o => o.r.DefaultIfEmpty(), (l, r) => new { l.sa, l.sd, ai = r })
            .Select(o => new VTVSessionRecord
            {
                AccessToken = o.sa.PrimaryKey,
                PushToken = o.sd == null ? string.Empty : o.sd.PushToken,
                Entry = o.sd == null ? EntryEnum.None : o.sd.Entry,
                IP = o.sd == null ? string.Empty : o.sd.IP,
                RoleType = o.sa.RoleType.ToString(),
                UserName = o.ai.UserName,
                Mobile = o.ai.Prefix + o.ai.Mobile,
                CreateTime = o.sa.CreateTime,
                UpdateTime = o.sa.UpdateTime,
                ExpiredTime = o.sa.ExpiredTime
            });

        var result = await base.PageByQueryableAsync<VTVSessionRecord, DTOSort>(query, input);

        return result;
    }
}