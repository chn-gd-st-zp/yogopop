namespace YogoPop.Component.Permission;

/// <summary>
/// 字段权限处理类型
/// </summary>
[Description("字段权限处理类型")]
public enum PermissionPropertyFailHandleEnum
{
    /// <summary>
    /// 默认、无
    /// </summary>
    [Description("默认、无")]
    None = 0,

    /// <summary>
    /// 错误抛出
    /// </summary>
    [Description("错误抛出")]
    Throw,

    /// <summary>
    /// 忽略处理
    /// </summary>
    [Description("忽略处理")]
    Ignore,

    /// <summary>
    /// 马赛克处理
    /// </summary>
    [Description("马赛克处理")]
    Mosaic,
}