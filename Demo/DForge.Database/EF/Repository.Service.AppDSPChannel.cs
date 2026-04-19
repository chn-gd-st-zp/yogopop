namespace DForge.Database.EF;

[DIModeForService(DIModeEnum.Exclusive, typeof(IAppDSPChannelRepository))]
public partial class AppDSPChannelRepository : RenewEFDBRepository<TBAppDSPChannel, string>, IAppDSPChannelRepository
{
    public async Task<Tuple<List<VTVDSPChannel>, int>> PageAsync(DTOAppDSPChannelPage input)
    {
        var query_channel = DBContext.GetQueryable<TBAppDSPChannel>();
        var query_project = DBContext.GetQueryable<TBAppProject>(false);

        if (input.Keyword.IsNotEmptyString())
            query_channel = query_channel.Where(o => o.Alias.Contains(input.Keyword));

        if (input.ProjectID.IsNotEmptyString())
            query_channel = query_channel.Where(o => o.ProjectID == input.ProjectID);

        if (input.DSP.IsNotNullOrDefault())
            query_channel = query_channel.Where(o => o.DSP == input.DSP);

        if (input.Alias.IsNotEmptyString())
            query_channel = query_channel.Where(o => o.Alias.Contains(input.Alias));

        if (input.Status.IsNotNullOrDefault())
            query_channel = query_channel.Where(o => o.Status == input.Status);

        var query = query_channel
            .GroupJoin(query_project, l => l.ProjectID, r => r.PrimaryKey, (l, r) => new { channel = l, r })
            .SelectMany(o => o.r.DefaultIfEmpty(), (l, r) => new { l.channel, project = r })
            .Where(o => o.project != null)
            .Select(o => new VTVDSPChannel
            {
                PrimaryKey = o.channel.PrimaryKey,
                ProjectID = o.channel.ProjectID,
                ProjectName = o.project.Name,
                DSP = o.channel.DSP,
                Alias = o.channel.Alias,
                Status = o.channel.Status,
            });

        var result = await base.PageByQueryableAsync<VTVDSPChannel, DTOSort>(query, input);

        return result;
    }
}