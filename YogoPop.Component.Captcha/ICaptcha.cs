namespace YogoPop.Component.Captcha;

public interface ICaptchaHandler : ITransient
{
    public Task<object> Generate();
}

public interface ICaptchaResult : ITransient
{
    public string Key { get; }

    public byte[] Image { get; }
}