namespace YogoPop.Core.CusEnum;

/// <summary>
/// 应用类型
/// </summary>
[Description("应用类型")]
[PublicEnum]
public enum AppTypeEnum
{
    /// <summary>
    /// 默认、无
    /// </summary>
    [Description("默认、无")]
    None = 0,

    /// <summary>
    /// 苹果
    /// </summary>
    [Description("苹果")]
    IOS,

    /// <summary>
    /// 安卓
    /// </summary>
    [Description("安卓")]
    Android,
}