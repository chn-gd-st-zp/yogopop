namespace YogoPop.Component.Auth;

[DIModeForSettings("AuthSettings", typeof(AuthSettings))]
public class AuthSettings : ICacheSettings
{
    /// <summary>
    /// 超时时间（分钟）
    /// </summary>
    public int TimeOutMinutes { get; set; }

    /// <summary>
    /// 缓存库
    /// </summary>
    public int DBIndex { get; set; }

    /// <summary>
    /// 前缀
    /// </summary>
    public string Prefix { get; set; }

    /// <summary>
    /// 会话前缀
    /// </summary>
    public string SessionPrefix { get; set; }

    /// <summary>
    /// 令牌前缀
    /// </summary>
    public string TokenPrefix { get; set; }

    /// <summary>
    /// AccessToken在Header中的标识
    /// </summary>
    public string AccessTokenKeyInHeader { get; set; }

    /// <summary>
    /// AccessToken在Header中的标识是否加密
    /// </summary>
    public bool AccessTokenEncrypt { get; set; }
}