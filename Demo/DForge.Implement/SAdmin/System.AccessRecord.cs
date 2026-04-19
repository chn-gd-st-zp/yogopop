namespace DForge.Implement.SAdmin;

[DIModeForService(DIModeEnum.ExclusiveByKeyed, typeof(Contract.SAdmin.ISysAccessRecordService<>), GlobalPermissionEnum.SAdmin)]
public partial class SysAccessRecordService<TTokenProvider> : ApiSAdminService<SysAccessRecordService<TTokenProvider>, ICache, TTokenProvider>, Contract.SAdmin.ISysAccessRecordService<TTokenProvider> where TTokenProvider : ITokenProvider
{
    [ActionPermission(GlobalPermissionEnum.AccessRecord_Single, GlobalPermissionEnum.AccessRecord)]
    public async Task<IServiceResult<DTOSysAccessRecordSingleResult>> Single(DTOSysAccessRecordSingle input)
    {
        var result = default(DTOSysAccessRecordSingleResult);

        using (var repository = InjectionContext.Resolve<ISysAccessRecordRepository>())
        {
            var obj = await repository.SingleAsync(input.PrimaryKey);
            if (obj == null)
                return result.Fail<DTOSysAccessRecordSingleResult, TargetNotFound>();

            result = obj.MapTo<DTOSysAccessRecordSingleResult>();
        }

        return result.Success<DTOSysAccessRecordSingleResult, LogicSucceed>();
    }

    [ActionPermission(GlobalPermissionEnum.AccessRecord_Page, GlobalPermissionEnum.AccessRecord)]
    public async Task<IServiceResult<DTOPageObj<DTOSysAccessRecordPageResult>>> Page(DTOSysAccessRecordPage input)
    {
        var result = default(DTOPageObj<DTOSysAccessRecordPageResult>);

        using (var repository = InjectionContext.Resolve<ISysAccessRecordRepository>())
            result = (await repository.PageAsync(input, string.Empty, RoleEnum.SuperAdmin, RoleEnum.Admin)).ToDTOPageObj(input, ep => ep.MapTo<DTOSysAccessRecordPageResult>());

        return result.Success<DTOPageObj<DTOSysAccessRecordPageResult>, LogicSucceed>();
    }
}