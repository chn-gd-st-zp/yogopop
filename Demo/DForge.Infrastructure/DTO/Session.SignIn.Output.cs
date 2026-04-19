namespace DForge.Infrastructure.DTO;

/// <summary>
/// 会话结果
/// </summary>
public abstract class DTOSessionResult : DTOOutput
{
    /// <summary>
    /// AccessToken
    /// </summary>
    [Description("AccessToken")]
    public string AccessToken { get; set; } = string.Empty;

    /// <summary>
    /// 用户名
    /// </summary>
    [Description("用户名")]
    public string UserName { get; set; } = string.Empty;

    /// <summary>
    /// 过期时间
    /// </summary>
    [Description("过期时间")]
    public DateTime ExpiredTime { get; set; } = DateTimeExtension.Now;
}