namespace DForge.Implement.SAdmin;

[DIModeForService(DIModeEnum.ExclusiveByKeyed, typeof(Contract.SAdmin.IAppDNSRecordService<>), GlobalPermissionEnum.SAdmin)]
public partial class AppDNSRecordService<TTokenProvider> : ApiSAdminService<AppDNSRecordService<TTokenProvider>, ICache, TTokenProvider>, Contract.SAdmin.IAppDNSRecordService<TTokenProvider> where TTokenProvider : ITokenProvider
{
    [ActionPermission(GlobalPermissionEnum.DNSRecord_List, GlobalPermissionEnum.DNSRecord)]
    public async Task<IServiceResult<List<DTOAppDNSRecordListResult>>> List(DTOAppDNSRecordList input)
    {
        var result = default(List<DTOAppDNSRecordListResult>);

        using (var repository = InjectionContext.Resolve<IAppDNSRecordRepository>())
            result = (await repository.ListAsync(input)).Select(o => o.MapTo<DTOAppDNSRecordListResult>()).ToList();

        return result.Success<List<DTOAppDNSRecordListResult>, LogicSucceed>();
    }

    [ActionPermission(GlobalPermissionEnum.DNSRecord_Set, GlobalPermissionEnum.DNSRecord)]
    public async Task<IServiceResult<bool>> Set(DTOAppDNSRecordSet input)
    {
        var result = false;

        using (var repository = InjectionContext.Resolve<IAppDNSRecordRepository>())
        {
            var channel = await repository.DBContext.SingleAsync<TBAppDSPChannel>(o => o.PrimaryKey == input.ChannelID);
            if (channel == null) return result.Fail<bool, TargetNotFound>();

            if (!channel.DSP.GetAttributes<DSOptAttribute>().Any(o => o.DSOpt == DSOptEnum.Analyse)) return result.Fail<bool, LogicFailed>();

            var domain = await repository.DBContext.SingleAsync<TBAppDomain>(o => o.PrimaryKey == input.DomainID);
            if (domain == null) return result.Fail<bool, TargetNotFound>();

            domain.AnalyseSrcID = domain.AnalyseChannelID == channel.PrimaryKey ? domain.AnalyseSrcID : string.Empty;

            var msgs = channel.GenerateMsg(domain, input.DNSRecords);
            if (msgs.IsNotEmpty()) InjectionContext.Resolve<DynSchPublisher>().RunAsync<TBAppDynSchRecord>(DynSchEnum.AnalyseSync, msgs.ToArray());
        }

        result = true;

        return result.Success<bool, LogicSucceed>();
    }
}