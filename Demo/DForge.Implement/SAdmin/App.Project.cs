namespace DForge.Implement.SAdmin;

[DIModeForService(DIModeEnum.ExclusiveByKeyed, typeof(Contract.SAdmin.IAppProjectService<>), GlobalPermissionEnum.SAdmin)]
public partial class AppProjectService<TTokenProvider> : ApiSAdminService<AppProjectService<TTokenProvider>, ICache, TTokenProvider>, Contract.SAdmin.IAppProjectService<TTokenProvider> where TTokenProvider : ITokenProvider
{
    [ActionPermission(GlobalPermissionEnum.Project_Create, GlobalPermissionEnum.Project, typeof(DTOAppProjectCreate), typeof(TBAppProject))]
    public async Task<IServiceResult<bool>> Create(DTOAppProjectCreate input)
    {
        var result = false;

        using (var repository = InjectionContext.Resolve<IAppProjectRepository>())
        {
            var obj = input.MapTo<TBAppProject>();

            result = await repository.CreateAsync(obj);
            if (!result) return result.Fail<bool, LogicFailed>();
        }

        return result.Success<bool, LogicSucceed>();
    }

    [ActionPermission(GlobalPermissionEnum.Project_Delete, GlobalPermissionEnum.Project, typeof(DTOPrimaryKeyRequired<string>), typeof(TBAppProject))]
    public async Task<IServiceResult<bool>> Delete(DTOPrimaryKeyRequired<string> input)
    {
        var result = false;

        using (var repository = InjectionContext.Resolve<IAppProjectRepository>())
        {
            var obj = await repository.SingleAsync(input.PrimaryKey);
            if (obj == null) return result.Fail<bool, TargetNotFound>();

            obj.Status = StatusEnum.Delete;

            result = await repository.UpdateAsync(obj);
            if (!result) return result.Fail<bool, LogicFailed>();
        }

        return result.Success<bool, LogicSucceed>();
    }

    [ActionPermission(GlobalPermissionEnum.Project_Update, GlobalPermissionEnum.Project, typeof(DTOAppProjectUpdate), typeof(TBAppProject))]
    public async Task<IServiceResult<bool>> Update(DTOAppProjectUpdate input)
    {
        var result = false;

        using (var repository = InjectionContext.Resolve<IAppProjectRepository>())
        {
            var obj = await repository.SingleAsync(input.PrimaryKey);
            if (obj == null) return result.Fail<bool, TargetNotFound>();

            obj = input.AdaptTo(obj);

            result = await repository.UpdateAsync(obj);
            if (!result) return result.Fail<bool, LogicFailed>();
        }

        return result.Success<bool, LogicSucceed>();
    }

    [ActionPermission(GlobalPermissionEnum.Project_Status, GlobalPermissionEnum.Project, typeof(DTOAppProjectUpdate), typeof(TBAppProject))]
    public async Task<IServiceResult<bool>> Status(DTOAppProjectStatus input)
    {
        var result = false;

        using (var repository = InjectionContext.Resolve<IAppProjectRepository>())
        {
            var obj = await repository.SingleAsync(input.PrimaryKey);
            if (obj == null) return result.Fail<bool, TargetNotFound>();

            obj = input.AdaptTo(obj);

            result = await repository.UpdateAsync(obj);
            if (!result) return result.Fail<bool, LogicFailed>();
        }

        return result.Success<bool, LogicSucceed>();
    }

    [ActionPermission(GlobalPermissionEnum.Project_Single, GlobalPermissionEnum.Project)]
    public async Task<IServiceResult<DTOAppProjectSingleResult>> Single(DTOPrimaryKeyRequired<string> input)
    {
        var result = default(DTOAppProjectSingleResult);

        using (var repository = InjectionContext.Resolve<IAppProjectRepository>())
        {
            var obj = await repository.DBContext.SingleAsync<TBAppProject>(o => o.PrimaryKey == input.PrimaryKey);
            if (obj == null) return result.Fail<DTOAppProjectSingleResult, TargetNotFound>();

            result = obj.MapTo<DTOAppProjectSingleResult>();
        }

        return result.Success<DTOAppProjectSingleResult, LogicSucceed>();
    }

    [ActionPermission(GlobalPermissionEnum.Project_Page, GlobalPermissionEnum.Project)]
    public async Task<IServiceResult<DTOPageObj<DTOAppProjectPageResult>>> Page(DTOAppProjectPage input)
    {
        var result = default(DTOPageObj<DTOAppProjectPageResult>);

        using (var repository = InjectionContext.Resolve<IAppProjectRepository>())
            result = (await repository.PageAsync(input)).ToDTOPageObj(input, ep => ep.MapTo<DTOAppProjectPageResult>());

        return result.Success<DTOPageObj<DTOAppProjectPageResult>, LogicSucceed>();
    }
}