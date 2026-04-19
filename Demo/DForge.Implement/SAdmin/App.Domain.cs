namespace DForge.Implement.SAdmin;

[DIModeForService(DIModeEnum.ExclusiveByKeyed, typeof(Contract.SAdmin.IAppDomainService<>), GlobalPermissionEnum.SAdmin)]
public partial class AppDomainService<TTokenProvider> : ApiSAdminService<AppDomainService<TTokenProvider>, ICache, TTokenProvider>, Contract.SAdmin.IAppDomainService<TTokenProvider> where TTokenProvider : ITokenProvider
{
    [ActionPermission(GlobalPermissionEnum.Domain_Page, GlobalPermissionEnum.Domain)]
    public async Task<IServiceResult<DTOPageObj<DTOAppDomainPageResult>>> Page(DTOAppDomainPage input)
    {
        var result = default(DTOPageObj<DTOAppDomainPageResult>);

        using (var repository = InjectionContext.Resolve<IAppDomainRepository>())
            result = (await repository.PageAsync(input)).ToDTOPageObj(input, ep => ep.MapTo<DTOAppDomainPageResult>());

        return result.Success<DTOPageObj<DTOAppDomainPageResult>, LogicSucceed>();
    }

    [ActionPermission(GlobalPermissionEnum.Domain_NSModify, GlobalPermissionEnum.Domain)]
    public async Task<IServiceResult<bool>> Set(DTOAppDomainNSSet input)
    {
        var result = false;

        using (var repository = InjectionContext.Resolve<IAppDomainRepository>())
        {
            var domains = await repository.DBContext.ListAsync<TBAppDomain>(o => input.DomainIDs.Contains(o.PrimaryKey) && o.RegistChannelID != string.Empty);
            if (domains.IsEmpty()) return result.Fail<bool, TargetNotFound>();

            var channelIDs = domains.GroupBy(o => o.RegistChannelID).Select(o => o.Key).ToArray();

            var channels = await repository.DBContext.ListAsync<TBAppDSPChannel>(o => channelIDs.Contains(o.PrimaryKey));
            if (channels.IsEmpty()) return result.Fail<bool, TargetNotFound>();

            var msgs = new List<DynSchMQMsg>();
            foreach (var channel in channels)
                msgs.AddRange(channel.GenerateMsg(domains.Where(o => o.RegistChannelID == channel.PrimaryKey), input.NameServers));

            if (msgs.IsNotEmpty()) InjectionContext.Resolve<DynSchPublisher>().RunAsync<TBAppDynSchRecord>(DynSchEnum.NameServerSync, msgs.ToArray());
        }

        result = true;

        return result.Success<bool, LogicSucceed>();
    }

    [ActionPermission(GlobalPermissionEnum.DomainSyncRecord_Apply, GlobalPermissionEnum.Domain, typeof(DTOAppDomainSyncCreate), typeof(TBAppDomain))]
    public async Task<IServiceResult<bool>> Create(DTOAppDomainSyncCreate input)
    {
        var result = false;

        using (var repository = InjectionContext.Resolve<IAppDSPChannelRepository>())
        {
            var domain = await repository.DBContext.SingleAsync<TBAppDomain>(o => o.PrimaryKey == input.PrimaryKey);
            if (domain == null) return result.Fail<bool, TargetNotFound>();

            var channel = await repository.DBContext.SingleAsync<TBAppDSPChannel>(o => o.PrimaryKey == input.ChannelID);
            if (channel == null) return result.Fail<bool, TargetNotFound>();

            domain.AnalyseSrcID = domain.AnalyseChannelID == channel.PrimaryKey ? domain.AnalyseSrcID : string.Empty;
            domain.AnalyseSrcTrusteeship = input.Trusteeship.IsEmptyString() ? domain.AnalyseSrcTrusteeship : input.Trusteeship;

            var msgs = channel.GenerateMsg(domain);
            if (msgs.IsEmpty()) return result.Fail<bool, TargetNotFound>();

            InjectionContext.Resolve<DynSchPublisher>().RunAsync<TBAppDynSchRecord>(DynSchEnum.DNSSync, msgs.ToArray());
        }

        result = true;

        return result.Success<bool, LogicSucceed>();
    }

    [ActionPermission(GlobalPermissionEnum.DomainSyncRecord_Page, GlobalPermissionEnum.Domain)]
    public async Task<IServiceResult<DTOPageObj<DTOAppDomainSyncPageResult>>> Page(DTOAppDomainSyncPage input)
    {
        var result = default(DTOPageObj<DTOAppDomainSyncPageResult>);

        using (var repository = InjectionContext.Resolve<IAppDynSchRecordRepository>())
            result = (await repository.PageAsync(input)).ToDTOPageObj(input, ep => ep.MapTo<DTOAppDomainSyncPageResult>());

        return result.Success<DTOPageObj<DTOAppDomainSyncPageResult>, LogicSucceed>();
    }
}