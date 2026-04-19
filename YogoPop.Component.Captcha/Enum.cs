namespace YogoPop.Component.Captcha;

/// <summary>
/// 验证码类型
/// </summary>
[Description("验证码类型")]
public enum CaptchaEnum
{
    /// <summary>
    /// 默认、无
    /// </summary>
    [Description("默认、无")]
    None = 0,

    /// <summary>
    /// 字符验证码
    /// </summary>
    [Description("字符验证码")]
    Code,

    /// <summary>
    /// 推理验证码
    /// </summary>
    [Description("推理验证码")]
    Inference,
}