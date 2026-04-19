namespace YogoPop.Component.Auth;

public class YogoSession<TTokenProvider> : IYogoSession<TTokenProvider> where TTokenProvider : ITokenProvider
{
    public YogoSession()
    {
        _authSettings = InjectionContext.Resolve<AuthSettings>();
        TokenProvider = InjectionContext.Resolve<TTokenProvider>();
    }

    private readonly AuthSettings _authSettings;

    public TTokenProvider TokenProvider { get; }

    public IYogoSessionContext SessionContext { get { return YogoSessionContextFactory.RestoreSessionContext().GetAwaiter().GetResult(); } }

    private string CurrentToken
    {
        get
        {
            try
            {
                if (_currentToken.IsEmptyString())
                {
                    var token1 = TokenProvider.CurrentToken;
                    var token2 = token1.IsEmptyString() ? string.Empty : token1;
                    var token = token2.ToLower() == "null" ? string.Empty : token2;
                    _currentToken = token;
                }

                return _currentToken;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
    private string _currentToken;

    public YogoSessionInfo CurrentAccount
    {
        get
        {
            string token = CurrentToken;
            if (token.IsEmptyString())
                throw new VEEmptyToken();

            if (_currentAccount == null)
            {
                var userTokenCache = Get(token);
                if (userTokenCache == null) throw new VENoLogin();

                _currentAccount = userTokenCache;
                _currentAccount.Extenstion(_authSettings.TimeOutMinutes);
                Set(_currentAccount);
            }

            if (_currentAccount != null)
            {
                var sessionContext = SessionContext;
                var type = typeof(TTokenProvider);
                var assemblyName = type.Assembly.ManifestModule.Name.Replace(".dll", string.Empty);
                sessionContext.TypeOfTokenProvider = type.FullName + ", " + assemblyName;
                sessionContext.SaveAsync().GetAwaiter().GetResult();
            }

            return _currentAccount;
        }
    }
    private YogoSessionInfo _currentAccount;

    public void Set(YogoSessionInfo info) => Set<YogoSessionInfo>(info).GetAwaiter().GetResult();

    public YogoSessionInfo Get(string accessToken) => Get<YogoSessionInfo>(accessToken).GetAwaiter().GetResult();

    public void Remove(string accessToken)
    {
        if (accessToken.IsEmptyString())
            return;

        if (_authSettings.AccessTokenEncrypt)
            accessToken = MD5.Encrypt(accessToken);

        using var diScope = InjectionContext.Root.CreateScope();
        using var cache = diScope.ResolveCache<ICache4Redis>(_authSettings);
        cache.DelAsync(_authSettings.TokenPrefix + accessToken).GetAwaiter().GetResult();
    }

    public void VerifyPermission(string permissionCode)
    {
        if (CurrentAccount.AccountInfo.RoleType == RoleEnum.SuperAdmin)
            return;

        if (CurrentAccount.PermissionCodes.Contains(permissionCode))
            return;

        throw new VENoPermission();
    }

    public async Task Set<TYogoSessionInfo>(TYogoSessionInfo info) where TYogoSessionInfo : YogoSessionInfo
    {
        if (info == null)
            return;

        var accessToken = info.AccessToken;
        if (_authSettings.AccessTokenEncrypt)
            accessToken = MD5.Encrypt(accessToken);

        using var diScope = InjectionContext.Root.CreateScope();
        using var cache = diScope.ResolveCache<ICache4Redis>(_authSettings);
        await cache.SetAsync(_authSettings.TokenPrefix + accessToken, info, info.ExpiredTime);
    }

    public async Task<TYogoSessionInfo> Get<TYogoSessionInfo>(string accessToken) where TYogoSessionInfo : YogoSessionInfo
    {
        var result = default(TYogoSessionInfo);

        if (accessToken.IsEmptyString())
            return result;

        if (_authSettings.AccessTokenEncrypt)
            accessToken = MD5.Encrypt(accessToken);

        using var diScope = InjectionContext.Root.CreateScope();
        using var cache = diScope.ResolveCache<ICache4Redis>(_authSettings);
        result = await cache.GetAsync<TYogoSessionInfo>(_authSettings.TokenPrefix + accessToken);

        return result;
    }
}