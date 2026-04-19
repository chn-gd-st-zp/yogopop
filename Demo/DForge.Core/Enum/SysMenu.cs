namespace DForge.Core.Enum;

/// <summary>
/// 菜单类型
/// </summary>
[PublicEnum]
public enum SysMenuEnum
{
    /// <summary>
    /// 默认、无
    /// </summary>
    [Description("默认、无")]
    None,

    /// <summary>
    /// 分组
    /// </summary>
    [Description("分组")]
    Group,

    /// <summary>
    /// 页面
    /// </summary>
    [Description("页面")]
    Page,

    /// <summary>
    /// 按钮
    /// </summary>
    [Description("按钮")]
    Btn,
}