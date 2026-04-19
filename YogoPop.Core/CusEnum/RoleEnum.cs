namespace YogoPop.Core.CusEnum;

/// <summary>
/// 角色类型
/// </summary>
[Description("角色类型")]
[PublicEnum]
public enum RoleEnum
{
    /// <summary>
    /// 默认、无
    /// </summary>
    [Description("默认、无")]
    None = 0,

    /// <summary>
    /// 超级管理员
    /// </summary>
    [Description("超级管理员")]
    SuperAdmin,

    /// <summary>
    /// 管理员
    /// </summary>
    [Description("管理员")]
    Admin,

    /// <summary>
    /// 用户
    /// </summary>
    [Description("用户")]
    User,
}