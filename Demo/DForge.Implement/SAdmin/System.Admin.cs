namespace DForge.Implement.SAdmin;

[DIModeForService(DIModeEnum.ExclusiveByKeyed, typeof(Contract.SAdmin.IAdminService<>), GlobalPermissionEnum.SAdmin)]
public class AdminService<TTokenProvider> : ApiSAdminService<AdminService<TTokenProvider>, ICache, TTokenProvider>, Contract.SAdmin.IAdminService<TTokenProvider> where TTokenProvider : ITokenProvider
{
    [ActionPermission(GlobalPermissionEnum.Admin_Create, GlobalPermissionEnum.Admin, typeof(DTOSysAdminCreate), typeof(TBAccountAdmin))]
    public async Task<IServiceResult<bool>> Create(DTOSysAdminCreate input)
    {
        var result = false;

        var now = DateTimeExtension.Now;

        var userName = await IdenHelper.CheckIdenCode(CacheLockEnum.UserNameLock, IdenHelper.UserNameExistenceFunc, input.UserName);
        if (userName.IsEmptyString())
            return result.Fail<bool, AccountDuplicated>();

        var secret = await IdenHelper.CheckIdenCode(CacheLockEnum.AccoutSecretLock, IdenHelper.AccountSecretExistenceFunc, IdenHelper.AccountSecretGenerateFunc);

        using (var repository = InjectionContext.Resolve<IAdminRepository>())
        {
            var role = await repository.DBContext.SingleAsync<TBSysRole>(o => o.PrimaryKey == input.RoleID);
            if (role == null)
                return result.Fail<bool, RoleNotFound>();

            if (role.Type != RoleEnum.Admin)
                return result.Fail<bool, RoleTypeInvalid>();

            if (await repository.DBContext.AnyAsync<TBAccountInfo>(o => (o.RoleType == RoleEnum.SuperAdmin || o.RoleType == RoleEnum.Admin) && o.UserName == userName))
                return result.Fail<bool, AccountDuplicated>();

            var obj = new TBAccountAdmin
            {
                RoleID = role.PrimaryKey,
                Secret = secret,
                Password = AccountHelper.EncryptPassword(input.Password, secret, true),
                UserName = userName,
                CreateTime = now,
                UpdateTime = now,
                Status = StatusEnum.Normal,
            };

            var objInfo = input.MapTo<TBAccountInfo>();
            objInfo.PrimaryKey = obj.PrimaryKey;
            objInfo.RoleType = role.Type;
            objInfo.UserType = role.SubType;
            objInfo.RoleID = role.PrimaryKey;
            objInfo.Secret = obj.Secret;
            objInfo.MFASecret = GoogleMFA.GenerateSecret();
            objInfo.Password = obj.Password;
            objInfo.UserName = obj.UserName;
            objInfo.Gender = GenderEnum.Secret;
            objInfo.CreateTime = obj.CreateTime;
            objInfo.UpdateTime = obj.UpdateTime;
            objInfo.Status = StatusEnum.Normal;

            result = await repository.CreateAsync(obj, objInfo);
            if (!result)
                return result.Fail<bool, LogicFailed>();

            IdenHelper.ReleaseIdenCode(CacheLockEnum.AccoutSecretLock, obj.Secret);
        }

        return result.Success<bool, LogicSucceed>();
    }

    [ActionPermission(GlobalPermissionEnum.Admin_Delete, GlobalPermissionEnum.Admin, typeof(DTOPrimaryKeyRequired<string>), typeof(TBAccountAdmin))]
    public async Task<IServiceResult<bool>> Delete(DTOPrimaryKeyRequired<string> input)
    {
        var result = false;

        using (var repository = InjectionContext.Resolve<IAdminRepository>())
        {
            var obj = await repository.SingleAsync(input.PrimaryKey);
            var objInfo = await repository.DBContext.SingleAsync<TBAccountInfo>(o => o.PrimaryKey == input.PrimaryKey);
            if (obj == null || objInfo == null)
                return result.Fail<bool, AccountNotFound>();

            obj.UpdateTime = DateTimeExtension.Now;
            obj.Status = StatusEnum.Delete;

            objInfo.UpdateTime = obj.UpdateTime;
            objInfo.Status = obj.Status;

            result = await repository.UpdateAsync(obj, objInfo);
            if (!result)
                return result.Fail<bool, LogicFailed>();
        }

        return result.Success<bool, LogicSucceed>();
    }

    [ActionPermission(GlobalPermissionEnum.Admin_Update, GlobalPermissionEnum.Admin, typeof(DTOSysAdminUpdate), typeof(TBAccountAdmin))]
    public async Task<IServiceResult<bool>> Update(DTOSysAdminUpdate input)
    {
        var result = false;

        using (var repository = InjectionContext.Resolve<IAdminRepository>())
        {
            var role = await repository.DBContext.SingleAsync<TBSysRole>(o => o.PrimaryKey == input.RoleID);
            if (role == null)
                return result.Fail<bool, RoleNotFound>();

            if (role.Type.NotIn(RoleEnum.Admin))
                return result.Fail<bool, RoleTypeInvalid>();

            var obj = await repository.SingleAsync(input.PrimaryKey);
            var objInfo = await repository.DBContext.SingleAsync<TBAccountInfo>(o => o.PrimaryKey == input.PrimaryKey);
            if (obj == null || objInfo == null)
                return result.Fail<bool, AccountNotFound>();

            obj.RoleID = input.RoleID;
            obj.UpdateTime = DateTimeExtension.Now;
            obj.Status = input.Status;

            objInfo.RoleID = obj.RoleID;
            objInfo.UpdateTime = obj.UpdateTime;
            objInfo.Status = obj.Status;

            result = await repository.UpdateAsync(obj, objInfo);
            if (!result)
                return result.Fail<bool, LogicFailed>();
        }

        return result.Success<bool, LogicSucceed>();
    }

    [ActionPermission(GlobalPermissionEnum.Admin_Status, GlobalPermissionEnum.Admin, typeof(DTOSysAdminStatus), typeof(TBAccountAdmin))]
    public async Task<IServiceResult<bool>> Status(DTOSysAdminStatus input)
    {
        var result = false;

        using (var repository = InjectionContext.Resolve<IAdminRepository>())
        {
            var obj = await repository.SingleAsync(input.PrimaryKey);
            if (obj == null)
                return result.Fail<bool, AccountNotFound>();

            obj.Status = input.Status;

            result = await repository.UpdateAsync(obj);
            if (!result)
                return result.Fail<bool, LogicFailed>();
        }

        return result.Success<bool, LogicSucceed>();
    }

    [ActionPermission(GlobalPermissionEnum.Admin_ResetPassword, GlobalPermissionEnum.Admin, typeof(DTOPrimaryKeyRequired<string>), typeof(TBAccountAdmin))]
    public async Task<IServiceResult<bool>> ResetPassword(DTOPrimaryKeyRequired<string> input)
    {
        var result = false;

        using (var repository = InjectionContext.Resolve<IAdminRepository>())
        {
            var obj = await repository.SingleAsync(input.PrimaryKey);
            var objInfo = await repository.DBContext.SingleAsync<TBAccountInfo>(o => o.PrimaryKey == input.PrimaryKey);
            if (obj == null || objInfo == null)
                return result.Fail<bool, AccountNotFound>();

            obj.Password = AccountHelper.EncryptPassword(SystemSettings.DefaultPassword, obj.Secret);
            obj.UpdateTime = DateTimeExtension.Now;

            objInfo.Password = obj.Password;
            objInfo.UpdateTime = obj.UpdateTime;

            result = await repository.UpdateAsync(obj, objInfo);
            if (!result)
                return result.Fail<bool, LogicFailed>();
        }

        return result.Success<bool, LogicSucceed>();
    }

    [ActionPermission(GlobalPermissionEnum.Admin_ResetMFA, GlobalPermissionEnum.Admin, typeof(DTOPrimaryKeyRequired<string>), typeof(TBAccountAdmin))]
    public async Task<IServiceResult<string>> ResetMFA(DTOPrimaryKeyRequired<string> input)
    {
        var result = string.Empty;

        using (var repository = InjectionContext.Resolve<IAdminRepository>())
        {
            var objInfo = await repository.DBContext.SingleAsync<TBAccountInfo>(o => o.PrimaryKey == input.PrimaryKey);
            if (objInfo == null)
                return result.Fail<string, AccountNotFound>();

            objInfo.MFASecret = GoogleMFA.GenerateSecret();

            if (!await repository.DBContext.UpdateAsync(objInfo))
                return result.Fail<string, LogicFailed>();

            result = GoogleMFA.GenerateQRCode(objInfo.MFASecret, objInfo.UserName);
        }

        return result.Success<string, LogicSucceed>();
    }

    [ActionPermission(GlobalPermissionEnum.Admin_Single, GlobalPermissionEnum.Admin)]
    public async Task<IServiceResult<DTOSysAdminSingleResult>> Single(DTOPrimaryKeyRequired<string> input)
    {
        var result = default(DTOSysAdminSingleResult);

        using (var repository = InjectionContext.Resolve<IAdminRepository>())
        {
            var obj = await repository.SingleAsync(input.PrimaryKey);
            var objInfo = await repository.DBContext.SingleAsync<TBAccountInfo>(o => o.PrimaryKey == input.PrimaryKey);
            if (obj == null || objInfo == null)
                return result.Fail<DTOSysAdminSingleResult, AccountNotFound>();

            var role = await repository.DBContext.SingleAsync<TBSysRole>(o => o.PrimaryKey == obj.RoleID);
            if (role == null)
                return result.Fail<DTOSysAdminSingleResult, RoleNotFound>();

            result = new VTVAccountAdmin
            {
                PrimaryKey = obj.PrimaryKey,
                RoleType = role.Type,
                RoleID = role.PrimaryKey,
                RoleName = role.Name,
                UserName = obj.UserName,
                MFASecret = objInfo.MFASecret,
                CreateTime = obj.CreateTime,
                Status = obj.Status
            }.MapTo<DTOSysAdminSingleResult>();
        }

        return result.Success<DTOSysAdminSingleResult, LogicSucceed>();
    }

    [ActionPermission(GlobalPermissionEnum.Admin_Page, GlobalPermissionEnum.Admin)]
    public async Task<IServiceResult<DTOPageObj<DTOSysAdminPageResult>>> Page(DTOSysAdminPage input)
    {
        var result = default(DTOPageObj<DTOSysAdminPageResult>);

        using (var repository = InjectionContext.Resolve<IAdminRepository>())
            result = (await repository.PageAsync(input)).ToDTOPageObj(input, ep => ep.MapTo<DTOSysAdminPageResult>());

        return result.Success<DTOPageObj<DTOSysAdminPageResult>, LogicSucceed>();
    }
}