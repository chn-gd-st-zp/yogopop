namespace YogoPop.Component.Permission;

/// <summary>
/// 权限节点分类
/// </summary>
[Description("权限节点分类")]
public enum PermissionTypeEnum
{
    /// <summary>
    /// 默认、无
    /// </summary>
    [Description("默认、无")]
    None = 0,

    /// <summary>
    /// 组
    /// </summary>
    [Description("组")]
    Group,

    /// <summary>
    /// 方法
    /// </summary>
    [Description("方法")]
    Action,

    /// <summary>
    /// 字段
    /// </summary>
    [Description("字段")]
    Property,

    /// <summary>
    /// 数据
    /// </summary>
    [Description("数据")]
    Data,
}