namespace DForge.Database.EF;

[DIModeForService(DIModeEnum.Exclusive, typeof(IAppDomainRepository))]
public partial class AppDomainRepository : RenewEFDBRepository<TBAppDomain, string>, IAppDomainRepository
{
    public async Task<Tuple<List<VTVDomain>, int>> PageAsync(DTOAppDomainPage input)
    {
        var query_domain = DBContext.GetQueryable<TBAppDomain>();
        var query_project = DBContext.GetQueryable<TBAppProject>(false);
        var query_channel = DBContext.GetQueryable<TBAppDSPChannel>(false);

        if (input.ProjectID.IsNotEmptyString())
            query_domain = query_domain.Where(o => o.ProjectID == input.ProjectID);

        if (input.Name.IsNotEmptyString())
            query_domain = query_domain.Where(o => o.Name.Contains(input.Name));

        if (input.Status.IsNotNullOrDefault())
            query_channel = query_channel.Where(o => o.Status == input.Status);

        if (input.CreateTimeRange != null)
        {
            if (input.CreateTimeRange.Begin.HasValue && input.CreateTimeRange.End.HasValue)
                query_domain = query_domain.Where(o => o.CreateTime.HasValue && input.CreateTimeRange.Begin <= o.CreateTime && o.CreateTime <= input.CreateTimeRange.End);
            else if (input.CreateTimeRange.Begin.HasValue)
                query_domain = query_domain.Where(o => o.CreateTime.HasValue && input.CreateTimeRange.Begin <= o.CreateTime);
            else if (input.CreateTimeRange.End.HasValue)
                query_domain = query_domain.Where(o => o.CreateTime.HasValue && o.CreateTime <= input.CreateTimeRange.End);
        }

        if (input.ExpiredTimeRange != null)
        {
            if (input.ExpiredTimeRange.Begin.HasValue && input.ExpiredTimeRange.End.HasValue)
                query_domain = query_domain.Where(o => o.ExpiredTime.HasValue && input.ExpiredTimeRange.Begin <= o.ExpiredTime && o.ExpiredTime <= input.ExpiredTimeRange.End);
            else if (input.ExpiredTimeRange.Begin.HasValue)
                query_domain = query_domain.Where(o => o.ExpiredTime.HasValue && input.ExpiredTimeRange.Begin <= o.ExpiredTime);
            else if (input.ExpiredTimeRange.End.HasValue)
                query_domain = query_domain.Where(o => o.ExpiredTime.HasValue && o.ExpiredTime <= input.ExpiredTimeRange.End);
        }

        var query = query_domain
            .GroupJoin(query_project, l => l.ProjectID, r => r.PrimaryKey, (l, r) => new { domain = l, r })
            .SelectMany(o => o.r.DefaultIfEmpty(), (l, r) => new { l.domain, project = r })
            .Where(o => o.project != null)
            .GroupJoin(query_channel, l => l.domain.RegistChannelID, r => r.PrimaryKey, (l, r) => new { l.domain, l.project, r })
            .SelectMany(o => o.r.DefaultIfEmpty(), (l, r) => new { l.domain, l.project, registChannel = r })
            .GroupJoin(query_channel, l => l.domain.AnalyseChannelID, r => r.PrimaryKey, (l, r) => new { l.domain, l.project, l.registChannel, r })
            .SelectMany(o => o.r.DefaultIfEmpty(), (l, r) => new { l.domain, l.project, l.registChannel, analyseChannel = r })
            .Select(o => new VTVDomain
            {
                PrimaryKey = o.domain.PrimaryKey,
                ProjectID = o.domain.ProjectID,
                ProjectName = o.project.Name,
                RegistChannelID = o.domain.RegistChannelID,
                RegistChannelAlias = o.registChannel != null ? o.registChannel.Alias : string.Empty,
                AnalyseChannelID = o.domain.RegistChannelID,
                AnalyseChannelAlias = o.analyseChannel != null ? o.analyseChannel.Alias : string.Empty,
                Name = o.domain.Name,
                NameServers = o.domain.NameServers,
                CreateTime = o.domain.CreateTime,
                ExpiredTime = o.domain.ExpiredTime,
                MainStatus = o.domain.MainStatus,
                SubStatus = o.domain.SubStatus,
                Status = o.domain.Status,
            });

        var result = await base.PageByQueryableAsync<VTVDomain, DTOSort>(query, input);

        return result;
    }
}