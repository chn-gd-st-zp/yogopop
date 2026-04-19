namespace DForge.Implement.SAdmin;

public abstract class SysSettingsService<TTokenProvider, TDBFSysSettings, TDTOSysSettingsResult> :
    ApiSAdminService<SysSettingsService<TTokenProvider, TDBFSysSettings, TDTOSysSettingsResult>, ICache, TTokenProvider>,
    Contract.SAdmin.ISysSettingsService<TTokenProvider, TDBFSysSettings, TDTOSysSettingsResult>
    where TTokenProvider : ITokenProvider
    where TDBFSysSettings : DBFSysSettings
    where TDTOSysSettingsResult : DTOSysSettingsResult
{
    public virtual async Task<IServiceResult<bool>> Create(DTOSysSettingsCreate input)
    {
        var result = false;

        input.Title = !input.Title.IsEmptyString() ? input.Title : input.Type.GetDesc();

        using (var repository = InjectionContext.Resolve<ISysSettingsRepository>())
        {
            var obj = input.MapTo<TBSysSettings>();
            obj.JsonData = input.ToJsonStr();
            obj.CreateTime = DateTimeExtension.Now;
            obj.SetSequence(!input.CurSequence.IsEmptyString() ? input.CurSequence : repository.DBContext.GetNextSequence<TBSysSettings>());

            result = await repository.CreateAsync(obj);
            if (!result)
                return result.Fail<bool, LogicFailed>();
        }

        return result.Success<bool, LogicSucceed>();
    }

    public virtual async Task<IServiceResult<bool>> Delete(DTOPrimaryKeyRequired<string> input)
    {
        var result = false;

        using (var repository = InjectionContext.Resolve<ISysSettingsRepository>())
        {
            var obj = await repository.SingleAsync(input.PrimaryKey);
            if (obj == null)
                return result.Fail<bool, TargetNotFound>();

            obj.Status = StatusEnum.Delete;

            result = await repository.UpdateAsync(obj);
            if (!result)
                return result.Fail<bool, LogicFailed>();
        }

        return result.Success<bool, LogicSucceed>();
    }

    public virtual async Task<IServiceResult<bool>> Update(DTOSysSettingsUpdate input)
    {
        var result = false;

        input.Title = !input.Title.IsEmptyString() ? input.Title : input.Type.GetDesc();

        using (var repository = InjectionContext.Resolve<ISysSettingsRepository>())
        {
            var obj = await repository.SingleAsync(input.PrimaryKey);
            if (obj == null)
                return result.Fail<bool, TargetNotFound>();

            var curSequence = obj.CurSequence;

            obj = input.AdaptTo(obj);
            obj.JsonData = input.ToJsonStr();
            obj.SetSequence(!input.CurSequence.IsEmptyString() ? input.CurSequence : curSequence);

            result = await repository.UpdateAsync(obj);
            if (!result)
                return result.Fail<bool, LogicFailed>();
        }

        return result.Success<bool, LogicSucceed>();
    }

    public virtual async Task<IServiceResult<bool>> Edit(DTOSysSettingsEdit input)
    {
        var result = false;

        input.Title = !input.Title.IsEmptyString() ? input.Title : input.Type.GetDesc();

        using (var repository = InjectionContext.Resolve<ISysSettingsRepository>())
        {
            var obj = await repository.SingleAsync(input.PrimaryKey);
            if (obj == null)
            {
                var obj_c = input.MapTo<TBSysSettings>();

                obj_c.JsonData = input.ToJsonStr();
                obj_c.CreateTime = DateTimeExtension.Now;
                obj.SetSequence(!input.CurSequence.IsEmptyString() ? input.CurSequence : repository.DBContext.GetNextSequence<TBSysSettings>());

                result = await repository.CreateAsync(obj_c);
            }
            else
            {
                var curSequence = obj.CurSequence;

                obj = input.AdaptTo(obj);
                obj.JsonData = input.ToJsonStr();
                obj.SetSequence(!input.CurSequence.IsEmptyString() ? input.CurSequence : curSequence);

                result = await repository.UpdateAsync(obj);
            }

            if (!result)
                return result.Fail<bool, LogicFailed>();
        }

        return result.Success<bool, LogicSucceed>();
    }

    public virtual async Task<IServiceResult<bool>> Status(DTOSysSettingsStatus input)
    {
        var result = false;

        using (var repository = InjectionContext.Resolve<ISysSettingsRepository>())
        {
            var obj = await repository.SingleAsync(input.PrimaryKey);
            if (obj == null)
                return result.Fail<bool, TargetNotFound>();

            obj.Status = input.Status;

            result = await repository.UpdateAsync(obj);
            if (!result)
                return result.Fail<bool, LogicFailed>();
        }

        return result.Success<bool, LogicSucceed>();
    }

    public virtual async Task<IServiceResult<TDTOSysSettingsResult>> Single(DTOSysSettingsSingle input)
    {
        var result = default(TDTOSysSettingsResult);

        using (var repository = InjectionContext.Resolve<ISysSettingsRepository>())
        {
            var obj = await repository.SingleAsync(o => o.Type == input.Type);
            if (obj == null)
                obj = new TBSysSettings();

            obj.PrimaryKey = input.Type.ToString();
            obj.Type = input.Type;
            obj.Title = input.Type.GetDesc();

            result = obj.Restore<TBSysSettings, TDBFSysSettings, TDTOSysSettingsResult>();
        }

        return result.Success<TDTOSysSettingsResult, LogicSucceed>();
    }

    public virtual async Task<IServiceResult<TDTOSysSettingsResult>> Single(DTOPrimaryKeyRequired<string> input)
    {
        var result = default(TDTOSysSettingsResult);

        using (var repository = InjectionContext.Resolve<ISysSettingsRepository>())
        {
            var obj = await repository.SingleAsync(input.PrimaryKey);
            if (obj == null)
                return result.Fail<TDTOSysSettingsResult, TargetNotFound>();

            result = obj.Restore<TBSysSettings, TDBFSysSettings, TDTOSysSettingsResult>();
        }

        return result.Success<TDTOSysSettingsResult, LogicSucceed>();
    }

    public virtual async Task<IServiceResult<List<TDTOSysSettingsResult>>> List(DTOSysSettingsList input)
    {
        var result = default(List<TDTOSysSettingsResult>);

        using (var repository = InjectionContext.Resolve<ISysSettingsRepository>())
        {
            result = repository.List(input)
                .Select(o => o.Restore<TBSysSettings, TDBFSysSettings, TDTOSysSettingsResult>())
                .ToList();

            result = result != null ? result : new List<TDTOSysSettingsResult>();
        }

        return result.Success<List<TDTOSysSettingsResult>, LogicSucceed>();
    }
}