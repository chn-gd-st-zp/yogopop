namespace YogoPop.Component.Captcha;

[DIModeForSettings("CodeCaptchaSettings", typeof(CodeCaptchaSettings))]
public class CodeCaptchaSettings : ISettings
{
    /// <summary>
    /// 字符位数
    /// </summary>
    public int CodeLength { get; set; }

    /// <summary>
    /// 图片宽
    /// </summary>
    public int ImageWidth { get; set; }

    /// <summary>
    /// 图片高
    /// </summary>
    public int ImageHeight { get; set; }

    /// <summary>
    /// 字体大小
    /// </summary>
    public int FontSize { get; set; }

    /// <summary>
    /// 字体集
    /// </summary>
    public string[] Fonts { get; set; }

    /// <summary>
    /// 颜色集
    /// </summary>
    public string[] Colors { get; set; }

    /// <summary>
    /// 干扰线条数
    /// </summary>
    public int LineQty { get; set; }

    /// <summary>
    /// 干扰点数
    /// </summary>
    public int PointQty { get; set; }
}