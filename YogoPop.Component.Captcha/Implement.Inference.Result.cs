namespace YogoPop.Component.Captcha;

public class InferenceCaptchaResult : ICaptchaResult
{
    public string Key { get { return _key; } }
    public string _key = Unique.GetGUID();

    public byte[] Image { get; set; }

    public int Index { get; set; }

    public int RowIndex { get; set; }

    public int ColIndex { get; set; }
}