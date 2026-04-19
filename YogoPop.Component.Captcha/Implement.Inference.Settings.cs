namespace YogoPop.Component.Captcha;

[DIModeForSettings("InferenceCaptchaSettings", typeof(InferenceCaptchaSettings))]
public class InferenceCaptchaSettings : ISettings
{
    /// <summary>
    /// 源图片地址
    /// </summary>
    public string SourceAddress { get; set; }

    /// <summary>
    /// 请求头
    /// </summary>
    public Dictionary<string, string> Headers { get; set; }

    /// <summary>
    /// 切分块数
    /// </summary>
    public int Pieces { get; set; }

    /// <summary>
    /// 行数
    /// </summary>
    public int Rows { get; set; }

    /// <summary>
    /// 列数
    /// </summary>
    public int Columns { get; set; }
}