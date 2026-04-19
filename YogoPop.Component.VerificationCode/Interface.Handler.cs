namespace YogoPop.Component.VerificationCode;

public interface IHandler : ITransient
{
    public string Provider { get; }

    public void LoadSettings(string json);

    public Task<bool> CreateAsync(string eventKey, RemoteChannelEnum remoteChannel, string prefix, string num, string message = default);

    public Task<bool> VerifyAsync(string eventKey, RemoteChannelEnum remoteChannel, string prefix, string num, string code);
}

public interface IHandler<TSettings> : IHandler where TSettings : class, IVCSettings
{
    public TSettings Settings { get; }
}