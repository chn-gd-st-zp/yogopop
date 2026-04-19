namespace YogoPop.Component.Captcha;

public class CodeCaptchaResult : ICaptchaResult
{
    public string Key { get; set; }

    public byte[] Image { get; set; }
}