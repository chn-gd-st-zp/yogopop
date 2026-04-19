namespace DForge.Implement.SAdmin;

[DIModeForService(DIModeEnum.ExclusiveByKeyed, typeof(Contract.SAdmin.IAppDSPChannelService<>), GlobalPermissionEnum.SAdmin)]
public partial class AppDSPChannelService<TTokenProvider> : ApiSAdminService<AppDSPChannelService<TTokenProvider>, ICache, TTokenProvider>, Contract.SAdmin.IAppDSPChannelService<TTokenProvider> where TTokenProvider : ITokenProvider
{
    [ActionPermission(GlobalPermissionEnum.DSPChannel_Create, GlobalPermissionEnum.DSPChannel, typeof(DTOAppDSPChannelCreate), typeof(TBAppDSPChannel))]
    public async Task<IServiceResult<bool>> Create(DTOAppDSPChannelCreate input)
    {
        var result = false;

        using (var repository = InjectionContext.Resolve<IAppDSPChannelRepository>())
        {
            var obj = input.MapTo<TBAppDSPChannel>();

            result = await repository.CreateAsync(obj);
            if (!result) return result.Fail<bool, LogicFailed>();
        }

        return result.Success<bool, LogicSucceed>();
    }

    [ActionPermission(GlobalPermissionEnum.DSPChannel_Delete, GlobalPermissionEnum.DSPChannel, typeof(DTOPrimaryKeyRequired<string>), typeof(TBAppDSPChannel))]
    public async Task<IServiceResult<bool>> Delete(DTOPrimaryKeyRequired<string> input)
    {
        var result = false;

        using (var repository = InjectionContext.Resolve<IAppDSPChannelRepository>())
        {
            var obj = await repository.SingleAsync(input.PrimaryKey);
            if (obj == null) return result.Fail<bool, TargetNotFound>();

            obj.Status = StatusEnum.Delete;

            result = await repository.UpdateAsync(obj);
            if (!result) return result.Fail<bool, LogicFailed>();
        }

        return result.Success<bool, LogicSucceed>();
    }

    [ActionPermission(GlobalPermissionEnum.DSPChannel_Update, GlobalPermissionEnum.DSPChannel, typeof(DTOAppDSPChannelUpdate), typeof(TBAppDSPChannel))]
    public async Task<IServiceResult<bool>> Update(DTOAppDSPChannelUpdate input)
    {
        var result = false;

        using (var repository = InjectionContext.Resolve<IAppDSPChannelRepository>())
        {
            var obj = await repository.SingleAsync(input.PrimaryKey);
            if (obj == null) return result.Fail<bool, TargetNotFound>();

            obj = input.AdaptTo(obj);

            result = await repository.UpdateAsync(obj);
            if (!result) return result.Fail<bool, LogicFailed>();
        }

        return result.Success<bool, LogicSucceed>();
    }

    [ActionPermission(GlobalPermissionEnum.DSPChannel_Status, GlobalPermissionEnum.DSPChannel, typeof(DTOAppDSPChannelUpdate), typeof(TBAppDSPChannel))]
    public async Task<IServiceResult<bool>> Status(DTOAppDSPChannelStatus input)
    {
        var result = false;

        using (var repository = InjectionContext.Resolve<IAppDSPChannelRepository>())
        {
            var obj = await repository.SingleAsync(input.PrimaryKey);
            if (obj == null) return result.Fail<bool, TargetNotFound>();

            obj = input.AdaptTo(obj);

            result = await repository.UpdateAsync(obj);
            if (!result) return result.Fail<bool, LogicFailed>();
        }

        return result.Success<bool, LogicSucceed>();
    }

    [ActionPermission(GlobalPermissionEnum.DSPChannel_Single, GlobalPermissionEnum.DSPChannel)]
    public async Task<IServiceResult<DTOAppDSPChannelSingleResult>> Single(DTOPrimaryKeyRequired<string> input)
    {
        var result = default(DTOAppDSPChannelSingleResult);

        using (var repository = InjectionContext.Resolve<IAppDSPChannelRepository>())
        {
            var obj = await repository.DBContext.SingleAsync<TBAppDSPChannel>(o => o.PrimaryKey == input.PrimaryKey);
            if (obj == null) return result.Fail<DTOAppDSPChannelSingleResult, TargetNotFound>();

            var project = await repository.DBContext.SingleAsync<TBAppProject>(o => o.PrimaryKey == obj.ProjectID);
            if (project == null) return result.Fail<DTOAppDSPChannelSingleResult, TargetNotFound>();

            result = obj.MapTo<DTOAppDSPChannelSingleResult>();
            result.ProjectName = project.Name;
        }

        return result.Success<DTOAppDSPChannelSingleResult, LogicSucceed>();
    }

    [ActionPermission(GlobalPermissionEnum.DSPChannel_Page, GlobalPermissionEnum.DSPChannel)]
    public async Task<IServiceResult<DTOPageObj<DTOAppDSPChannelPageResult>>> Page(DTOAppDSPChannelPage input)
    {
        var result = default(DTOPageObj<DTOAppDSPChannelPageResult>);

        using (var repository = InjectionContext.Resolve<IAppDSPChannelRepository>())
            result = (await repository.PageAsync(input)).ToDTOPageObj(input, ep => ep.MapTo<DTOAppDSPChannelPageResult>());

        return result.Success<DTOPageObj<DTOAppDSPChannelPageResult>, LogicSucceed>();
    }

    [ActionPermission(GlobalPermissionEnum.DSPChannelSyncRecord_Apply, GlobalPermissionEnum.DSPChannel, typeof(DTOPrimaryKeyRequired<string>), typeof(TBAppDynSchRecord))]
    public async Task<IServiceResult<bool>> Create(DTOPrimaryKeyRequired<string> input)
    {
        var result = false;

        using (var repository = InjectionContext.Resolve<IAppDSPChannelRepository>())
        {
            var obj = await repository.DBContext.SingleAsync<TBAppDSPChannel>(o => o.PrimaryKey == input.PrimaryKey);
            if (obj == null) return result.Fail<bool, TargetNotFound>();

            var msgs = obj.GenerateMsg();
            if (msgs.IsEmpty()) return result.Fail<bool, TargetNotFound>();

            InjectionContext.Resolve<DynSchPublisher>().RunAsync<TBAppDynSchRecord>(DynSchEnum.DomainSync, msgs.ToArray());
        }

        result = true;

        return result.Success<bool, LogicSucceed>();
    }

    [ActionPermission(GlobalPermissionEnum.DSPChannelSyncRecord_Page, GlobalPermissionEnum.DSPChannel)]
    public async Task<IServiceResult<DTOPageObj<DTOAppDSPChannelSyncPageResult>>> Page(DTOAppDSPChannelSyncPage input)
    {
        var result = default(DTOPageObj<DTOAppDSPChannelSyncPageResult>);

        using (var repository = InjectionContext.Resolve<IAppDynSchRecordRepository>())
            result = (await repository.PageAsync(input)).ToDTOPageObj(input, ep => ep.MapTo<DTOAppDSPChannelSyncPageResult>());

        return result.Success<DTOPageObj<DTOAppDSPChannelSyncPageResult>, LogicSucceed>();
    }
}