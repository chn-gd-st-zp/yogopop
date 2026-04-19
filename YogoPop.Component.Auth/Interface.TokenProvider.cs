namespace YogoPop.Component.Auth;

public interface ITokenProvider : ITransient
{
    public ProtocolEnum Protocol { get; }

    public string CurrentToken { get; }
}