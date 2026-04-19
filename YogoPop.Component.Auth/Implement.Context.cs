namespace YogoPop.Component.Auth;

public class YogoSessionContextBase : IYogoSessionContext
{
    public virtual string ContextID { get; set; }

    public virtual string TypeOfTokenProvider { get; set; }

    public virtual OperationTypeEnum OperationType { get; set; }

    public virtual void RestoreContextID() { }

    public async Task SaveAsync()
    {
        var authSettings = InjectionContext.Resolve<AuthSettings>();
        using var diScope = InjectionContext.Root.CreateScope();
        using var cache = diScope.ResolveCache<ICache4Redis>(authSettings);
        await cache.SetAsync(authSettings.SessionPrefix + ContextID, this.ToJson(), TimeSpan.FromMinutes(5));
    }
}