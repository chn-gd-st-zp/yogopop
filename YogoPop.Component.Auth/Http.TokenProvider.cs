namespace YogoPop.Component.Auth;

[DIModeForService(DIModeEnum.AsSelf)]
[DIModeForService(DIModeEnum.ExclusiveByKeyed, typeof(ITokenProvider), ProtocolEnum.HTTP)]
public class HTTPTokenProvider : ITokenProvider
{
    public ProtocolEnum Protocol { get { return ProtocolEnum.HTTP; } }

    public string CurrentToken
    {
        get
        {
            var iContext = InjectionContext.Resolve<IHttpContextAccessor>();
            if (iContext == null)
                return string.Empty;

            var context = iContext.HttpContext;
            if (context == null)
                return string.Empty;

            var authSettings = InjectionContext.Resolve<AuthSettings>();
            var token1 = context.Request.Headers[authSettings.AccessTokenKeyInHeader];
            if (token1.IsEmptyString())
                return string.Empty;

            var token2 = token1.ToString();
            if (token2.ToLower() == "null".ToLower())
                return string.Empty;

            return token2;
        }
    }
}