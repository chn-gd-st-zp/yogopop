namespace DForge.Database.EF;

[DIModeForService(DIModeEnum.Exclusive, typeof(IAppDNSRecordRepository))]
public partial class AppDNSRecordRepository : RenewEFDBRepository<TBAppDNSRecord, string>, IAppDNSRecordRepository
{
    public async Task<List<VTVDNSRecord>> ListAsync(DTOAppDNSRecordList input)
    {
        var query_dnsRecord = DBContext.GetQueryable<TBAppDNSRecord>().Where(o => o.DomainID == input.DomainID);
        var query_project = DBContext.GetQueryable<TBAppProject>(false);
        var query_domain = DBContext.GetQueryable<TBAppDomain>(false);

        if (input.Type.IsNotNullOrDefault())
            query_dnsRecord = query_dnsRecord.Where(o => o.Type == input.Type);

        var query = query_dnsRecord
            .GroupJoin(query_project, l => l.ProjectID, r => r.PrimaryKey, (l, r) => new { dnsRecord = l, r })
            .SelectMany(o => o.r.DefaultIfEmpty(), (l, r) => new { l.dnsRecord, project = r })
            .Where(o => o.project != null)
            .GroupJoin(query_domain, l => l.dnsRecord.DomainID, r => r.PrimaryKey, (l, r) => new { l.dnsRecord, l.project, r })
            .SelectMany(o => o.r.DefaultIfEmpty(), (l, r) => new { l.dnsRecord, l.project, domain = r })
            .Where(o => o.domain != null)
            .Select(o => new VTVDNSRecord
            {
                PrimaryKey = o.dnsRecord.PrimaryKey,
                ProjectID = o.dnsRecord.ProjectID,
                ProjectName = o.project.Name,
                DomainID = o.dnsRecord.DomainID,
                DomainName = o.domain.Name,
                Type = o.dnsRecord.Type,
                Source = o.dnsRecord.Source,
                Target = o.dnsRecord.Target,
                TTL = o.dnsRecord.TTL,
                Priority = o.dnsRecord.Priority,
                Proxied = o.dnsRecord.Proxied,
                IPv4Only = o.dnsRecord.IPv4Only,
                IPv6Only = o.dnsRecord.IPv6Only,
                Remark = o.dnsRecord.Remark,
                Tags = o.dnsRecord.Tags,
                SrcID = o.dnsRecord.SrcID,
            });

        var result = await base.ListByQueryableAsync<VTVDNSRecord, DTOSort>(query, input);

        return result;
    }
}