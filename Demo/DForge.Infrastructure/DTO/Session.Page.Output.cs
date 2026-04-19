namespace DForge.Infrastructure.DTO;

/// <summary>
/// 会话
/// </summary>
public class DTOSessionPageResult : DTOOutput
{
    /// <summary>
    /// AccessToken
    /// </summary>
    [Description("AccessToken")]
    public string AccessToken { get; set; }

    /// <summary>
    /// 推送标识
    /// </summary>
    [Description("推送标识")]
    public string PushToken { get; set; }

    /// <summary>
    /// 入口
    /// </summary>
    [Description("入口")]
    public string Entry { get; set; }

    /// <summary>
    /// IP
    /// </summary>
    [Description("IP")]
    public string IP { get; set; }

    /// <summary>
    /// 角色类型
    /// </summary>
    [Description("角色类型")]
    public string RoleType { get; set; }

    /// <summary>
    /// 用户名
    /// </summary>
    [Description("用户名")]
    public string UserName { get; set; }

    /// <summary>
    /// 手机
    /// </summary>
    [Description("手机")]
    public string Mobile { get; set; }

    /// <summary>
    /// 邮箱
    /// </summary>
    [Description("邮箱")]
    public string Email { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    [Description("创建时间")]
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [Description("修改时间")]
    public DateTime UpdateTime { get; set; }

    /// <summary>
    /// 过期时间
    /// </summary>
    [Description("过期时间")]
    public DateTime ExpiredTime { get; set; }
}