namespace DForge.Core.Enum;

/// <summary>
/// 系统设置分类
/// </summary>
[PublicEnum(true)]
public enum SysSettingsEnum
{
    [Description("默认、无")]
    [PublicEnum]
    None = 0,

    [Description("支付域名")]
    [InternalEnum, UserVisibleEnum]
    PayDomain,
}