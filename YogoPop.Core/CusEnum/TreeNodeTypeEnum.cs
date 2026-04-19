namespace YogoPop.Core.CusEnum;

/// <summary>
/// 树形节点类型
/// </summary>
[Description("树形节点类型")]
public enum TreeNodeTypeEnum
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
    /// 节点
    /// </summary>
    [Description("节点")]
    Node,
}