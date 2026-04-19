namespace YogoPop.Core.CusEnum;

/// <summary>
/// 入口
/// </summary>
[Description("入口")]
[PublicEnum]
public enum EntryEnum
{
    /// <summary>
    /// 默认、无
    /// </summary>
    [Description("默认、无")]
    None = 0,

    /// <summary>
    /// 其他
    /// </summary>
    [Description("其他")]
    Other = 1,

    /// <summary>
    /// PC
    /// </summary>
    [Description("PC")]
    PC = 2,

    /// <summary>
    /// H5
    /// </summary>
    [Description("H5")]
    H5 = 3,

    /// <summary>
    /// 苹果APP
    /// </summary>
    [Description("苹果APP")]
    IOSAPP = 4,

    /// <summary>
    /// 安卓APP
    /// </summary>
    [Description("安卓APP")]
    AndroidAPP = 5,

    /// <summary>
    /// 微信小程序
    /// </summary>
    [Description("微信小程序")]
    WeChatMicoApp = 6,
}