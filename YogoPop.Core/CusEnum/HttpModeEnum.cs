namespace YogoPop.Core.CusEnum;

/// <summary>
/// HttpMode
/// </summary>
[Description("HttpMode")]
public enum HttpModeEnum
{
    /// <summary>
    /// 默认、无
    /// </summary>
    [Description("默认、无")]
    None = 0,

    /// <summary>
    /// Get
    /// </summary>
    [Description("Get")]
    GET,

    /// <summary>
    /// Post
    /// </summary>
    [Description("Post")]
    POST,
}