namespace YogoPop.Component.Auth;

public interface IYogoSession : ITransient
{
    public IYogoSessionContext SessionContext { get; }

    public YogoSessionInfo CurrentAccount { get; }

    public void Set(YogoSessionInfo info);

    public YogoSessionInfo Get(string accessToken);

    public void Remove(string accessToken);

    public void VerifyPermission(string permissionCode);
}

public interface IYogoSession<TTokenProvider> : IYogoSession
    where TTokenProvider : ITokenProvider
{
    public TTokenProvider TokenProvider { get; }
}