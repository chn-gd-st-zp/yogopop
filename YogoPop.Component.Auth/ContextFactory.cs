namespace YogoPop.Component.Auth;

public static class YogoSessionContextFactory
{
    public static async Task<IYogoSessionContext> RestoreSessionContext()
    {
        var context = default(IYogoSessionContext);

        using (var diScope = InjectionContext.Root.CreateScope())
        {
            var authSettings = diScope.Resolve<AuthSettings>();
            if (authSettings == null) return context;

            foreach (var kv in typeof(ProtocolEnum).ToDictionary())
            {
                context = diScope.ResolveByKeyed<IYogoSessionContext>(kv.Key.ToEnum<ProtocolEnum>());
                if (context != null) break;
            }

            if (context == null) return context;

            context.RestoreContextID();
            using (var cache = diScope.ResolveCache<ICache4Redis>(authSettings))
            {
                var contextInCache = await cache.GetAsync<YogoSessionContextBase>(authSettings.SessionPrefix + context.ContextID);
                if (contextInCache != null)
                    context = contextInCache;
            }
        }

        return context;
    }

    public static async Task<IYogoSession> RestoreSession(this IYogoSessionContext sessionContext)
    {
        if (sessionContext == null)
            sessionContext = await RestoreSessionContext();

        if (sessionContext == null || sessionContext.TypeOfTokenProvider.IsEmptyString()) return null;

        var type = Type.GetType(sessionContext.TypeOfTokenProvider);
        if (type == null) return null;

        var tokenProvider = InjectionContext.Resolve(type) as ITokenProvider;
        if (tokenProvider == null || tokenProvider.Protocol == ProtocolEnum.None) return null;

        return InjectionContext.ResolveByKeyed<IYogoSession>(tokenProvider.Protocol);
    }

    public static async Task<IYogoSession> RestoreSession() => await RestoreSession(null);
}