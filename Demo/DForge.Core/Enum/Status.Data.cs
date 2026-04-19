namespace DForge.Core.Enum;

/// <summary>
/// 数据状态
/// </summary>
[PublicEnum]
[DeleteDeclare(Delete)]
public enum StatusEnum
{
    /// <summary>
    /// 默认、无
    /// </summary>
    [Description("默认、无")]
    None,

    /// <summary>
    /// 正常
    /// </summary>
    [Description("正常")]
    Normal,

    /// <summary>
    /// 禁用
    /// </summary>
    [Description("禁用")]
    Disable,

    /// <summary>
    /// 删除
    /// </summary>
    [Description("删除")]
    Delete,
}