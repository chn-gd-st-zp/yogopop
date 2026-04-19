namespace DForge.Implement.SAdmin;

[DIModeForService(DIModeEnum.ExclusiveByKeyed, typeof(Contract.SAdmin.ISessionService<>), GlobalPermissionEnum.SAdmin)]
public partial class SessionService<TTokenProvider> : ApiSAdminService<SessionService<TTokenProvider>, ICache, TTokenProvider>, Contract.SAdmin.ISessionService<TTokenProvider> where TTokenProvider : ITokenProvider
{
    public async Task<IServiceResult<DTOSessionSAdminSignInResult>> SignIn(DTOSessionSAdminSignIn input)
    {
        var result = default(DTOSessionSAdminSignInResult);

        using (var sysPermissionRepository = InjectionContext.Resolve<ISysPermissionRepository>())
        using (var accountInfoRepository = InjectionContext.Resolve<IAccountInfoRepository>())
        using (var sessionRepository = InjectionContext.Resolve<ISessionRepository>())
        {
            var accountInfo = accountInfoRepository.SingleByRoleAndAccount(input.RoleTypes, input.Account);
            if (accountInfo == null || AccountHelper.EncryptPassword(input.Password, accountInfo.Secret, true) != accountInfo.Password)
                return result.Fail<DTOSessionSAdminSignInResult, SignInFailed>();

            if (accountInfo.Status == StatusEnum.Disable)
                return result.Fail<DTOSessionSAdminSignInResult, AccountDisabled>();

            var role = await sessionRepository.DBContext.SingleAsync<TBSysRole>(o => o.PrimaryKey == accountInfo.RoleID);
            if (role == null)
                return result.Fail<DTOSessionSAdminSignInResult, RoleNotFound>();

#if DEBUG
            if (input.MFACode.IsNotEmptyString() && !GoogleMFA.Verify(accountInfo.MFASecret, input.MFACode))
                return result.Fail<DTOSessionSAdminSignInResult, SignInFailed>();
#else
            if (!GoogleMFA.Verify(accountInfo.MFASecret, input.MFACode))
                return result.Fail<DTOSessionSAdminSignInResult, SignInFailed>();

            var sessionList = await sessionRepository.DBContext.ListAsync<TBSessionAccount>(o => o.AccountID == accountInfo.PrimaryKey && o.ExpiredTime > DateTimeExtension.Now);
            foreach (var session in sessionList)
            {
                await AccountHelper.Offline(
                    session.PrimaryKey,
                    (accessToken) => { Session.Remove(accessToken); }
                );
            }
#endif

            var sessionInfo = new YogoSessionInfo();

            sessionInfo.AccessToken = Unique.GetID();
            sessionInfo.PermissionCodes = sysPermissionRepository.ListByRoleCodes(new string[] { role.CurNode }).Select(o => o.CurNode).OrderBy(o => o).ToArray();

            sessionInfo.DeviceInfo.Entry = input.Entry;
            sessionInfo.DeviceInfo.IP = NetTool.GetIP();

            sessionInfo.AccountInfo.RoleType = accountInfo.RoleType;
            sessionInfo.AccountInfo.RoleCodes = new string[] { role.CurNode };
            sessionInfo.AccountInfo.AccountID = accountInfo.PrimaryKey;

            sessionInfo.SetExpiredTime();

            var sd = sessionInfo.DeviceInfo.MapTo<TBSessionDevice>();
            sd.PrimaryKey = sessionInfo.AccessToken;

            var sa = sessionInfo.AccountInfo.MapTo<TBSessionAccount>();
            sa.PrimaryKey = sessionInfo.AccessToken;
            sa.RoleCodes = sessionInfo.AccountInfo.RoleCodes.ToString(',');

            if (!await sessionRepository.CreateAsync(sd, sa))
                return result.Fail<DTOSessionSAdminSignInResult, LogicFailed>();

            Session.Set(sessionInfo);

            result = sessionInfo.MapTo<DTOSessionSAdminSignInResult>();
            result.RoleCodes = sessionInfo.AccountInfo.RoleCodes;
            result.Menus = await Menus();
        }

        return result.Success<DTOSessionSAdminSignInResult, LogicSucceed>();
    }

    public async Task<IServiceResult<bool>> SignOut()
    {
        return await AccountHelper.Offline(
            Session.CurrentAccount.AccessToken,
            (accessToken) => { Session.Remove(accessToken); }
        );
    }

    public async Task<IServiceResult<DTOSessionSAdminSignInResult>> Get()
    {
        var result = new DTOSessionSAdminSignInResult();

        using (var repository = InjectionContext.Resolve<ISessionRepository>())
        {
            if (!await repository.UpdateAsync(Session.CurrentAccount.AccessToken, Session.CurrentAccount.ExpiredTime, Session.CurrentAccount.UpdateTime))
                return result.Fail<DTOSessionSAdminSignInResult, LogicFailed>();
        }

        result = Session.CurrentAccount.MapTo<DTOSessionSAdminSignInResult>();
        result.RoleCodes = Session.CurrentAccount.AccountInfo.RoleCodes;
        result.Menus = await Menus();

        return result.Success<DTOSessionSAdminSignInResult, LogicSucceed>();
    }

    public async Task<IServiceResult<bool>> UpdatePassword(DTOSessionUpdatePassword input)
    {
        var result = false;

        using (var repository = InjectionContext.Resolve<IAdminRepository>())
        {
            var obj = await repository.SingleAsync(CurAccountID);
            var objInfo = await repository.DBContext.SingleAsync<TBAccountInfo>(o => o.PrimaryKey == CurAccountID);
            if (obj == null || objInfo == null)
                return result.Fail<bool, AccountNotFound>();

            if (obj.Password != AccountHelper.EncryptPassword(input.OldPassword, obj.Secret, true))
                return result.Fail<bool, OldPasswordInvalid>();

            obj.Password = AccountHelper.EncryptPassword(input.NewPassword, obj.Secret, true);
            obj.UpdateTime = DateTimeExtension.Now;

            objInfo.Password = obj.Password;
            objInfo.UpdateTime = obj.UpdateTime;

            result = await repository.UpdateAsync(obj, objInfo);
            if (!result)
                return result.Fail<bool, LogicFailed>();
        }

        return result.Success<bool, LogicSucceed>();
    }

    [ActionPermission(GlobalPermissionEnum.Session_Page, GlobalPermissionEnum.Session)]
    public async Task<IServiceResult<DTOPageObj<DTOSessionPageResult>>> Page(DTOSessionPage input)
    {
        var result = default(DTOPageObj<DTOSessionPageResult>);

        using (var repository = InjectionContext.Resolve<ISessionRepository>())
            result = (await repository.PageAsync(input)).ToDTOPageObj(input, ep => ep.MapTo<DTOSessionPageResult>());

        return result.Success<DTOPageObj<DTOSessionPageResult>, LogicSucceed>();
    }

    [ActionPermission(GlobalPermissionEnum.Session_Kick, GlobalPermissionEnum.Session, typeof(DTOSessionKick), typeof(TBSessionAccount))]
    public async Task<IServiceResult<bool>> Kick(DTOSessionKick input)
    {
        return await AccountHelper.Offline(
            Session.CurrentAccount.AccessToken,
            (accessToken) => { Session.Remove(accessToken); }
        );
    }
}