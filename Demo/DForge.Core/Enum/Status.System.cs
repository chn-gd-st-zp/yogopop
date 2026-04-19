namespace DForge.Core.Enum;

/// <summary>
/// 系统维护类型
/// </summary>
[Description("系统维护类型")]
[InternalEnum]
public enum SysStatusEnum
{
    /// <summary>
    /// 运行中
    /// </summary>
    [Description("运行中")]
    Running = 0,

    /// <summary>
    /// 维护中
    /// </summary>
    [Description("维护中")]
    Maintaining = 1,
}