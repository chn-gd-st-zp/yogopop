namespace YogoPop.Component.Captcha;

[DIModeForService(DIModeEnum.ExclusiveByKeyed, typeof(ICaptchaHandler), CaptchaEnum.Code)]
public class CodeCaptcha : ICaptchaHandler
{
    private readonly SimpleCaptcha.ICaptcha _captcha;

    public CodeCaptcha(SimpleCaptcha.ICaptcha captcha) { _captcha = captcha; }

    public async Task<object> Generate()
    {
        var info = _captcha.Generate(Unique.GetGUID());
        return new CodeCaptchaResult { Key = info.CaptchaCode, Image = info.CaptchaByteData };
    }
}