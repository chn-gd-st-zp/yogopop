namespace YogoPop.Core.CusEnum;

/// <summary>
/// 操作类型
/// </summary>
[Description("操作类型")]
[PublicEnum]
public enum OperationTypeEnum
{
    /// <summary>
    /// 默认、无
    /// </summary>
    [Description("默认、无")]
    None = 0,

    /// <summary>
    /// 新增
    /// </summary>
    [Description("新增")]
    Create,

    /// <summary>
    /// 删除
    /// </summary>
    [Description("删除")]
    Delete,

    /// <summary>
    /// 修改
    /// </summary>
    [Description("修改")]
    Update,

    /// <summary>
    /// 查询
    /// </summary>
    [Description("查询")]
    Search,
}