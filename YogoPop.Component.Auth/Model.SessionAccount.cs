namespace YogoPop.Component.Auth;

public class YogoSessionAccount
{
    /// <summary>
    /// 角色类型
    /// </summary>
    public RoleEnum RoleType { get; set; } = RoleEnum.None;

    /// <summary>
    /// 角色代码
    /// </summary>
    public string[] RoleCodes { get; set; } = new string[0];

    /// <summary>
    /// 账号ID
    /// </summary>
    public string AccountID { get; set; } = string.Empty;

    /// <summary>
    /// 用户名
    /// </summary>
    public string UserName { get; set; } = string.Empty;

    /// <summary>
    /// 昵称
    /// </summary>
    public string NickName { get; set; } = string.Empty;

    /// <summary>
    /// 语言
    /// </summary>
    public string Language { get; set; } = string.Empty;

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreateTime { get; set; } = DateTimeExtension.Now;

    /// <summary>
    /// 刷新时间
    /// </summary>
    public DateTime UpdateTime { get; set; } = DateTimeExtension.Now;

    /// <summary>
    /// 过期时间
    /// </summary>
    public DateTime ExpiredTime { get; set; } = DateTimeExtension.Now;
}