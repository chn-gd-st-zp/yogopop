namespace YogoPop.Core.Injection;

/// <summary>
/// 注入类型
/// </summary>
[Description("注入类型")]
public enum DIModeEnum
{
    /// <summary>
    /// 默认、无
    /// </summary>
    [Description("默认、无")]
    None = 0,

    /// <summary>
    /// 自身
    /// </summary>
    [Description("自身")]
    AsSelf,

    /// <summary>
    /// 自动接口
    /// </summary>
    [Description("自动接口")]
    AsImpl,

    /// <summary>
    /// 指定对象
    /// </summary>
    [Description("指定对象")]
    Exclusive,

    /// <summary>
    /// 指定对象-ByNamed
    /// </summary>
    [Description("指定对象-ByNamed")]
    ExclusiveByNamed,

    /// <summary>
    /// 指定对象-Bykeyed
    /// </summary>
    [Description("指定对象-Bykeyed")]
    ExclusiveByKeyed,
}

/// <summary>
/// 注入标识来源
/// </summary>
[Description("注入标识来源")]
public enum DIKeyedNamedFromEnum
{
    /// <summary>
    /// 默认、无
    /// </summary>
    [Description("默认、无")]
    None = 0,

    /// <summary>
    /// 从属性
    /// </summary>
    [Description("从属性")]
    FromProperty,
}