namespace YogoPop.Core.CusEnum;

/// <summary>
/// 通讯渠道
/// </summary>
[Description("通讯渠道")]
[PublicEnum]
public enum RemoteChannelEnum
{
    /// <summary>
    /// 默认、无
    /// </summary>
    [Description("默认、无")]
    None = 0,

    /// <summary>
    /// 手机
    /// </summary>
    [Description("手机")]
    Mobile,

    /// <summary>
    /// 邮箱
    /// </summary>
    [Description("邮箱")]
    Email,
}