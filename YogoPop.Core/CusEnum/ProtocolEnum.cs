namespace YogoPop.Core.CusEnum;

/// <summary>
/// 协议类型
/// </summary>
[Description("协议类型")]
public enum ProtocolEnum
{
    /// <summary>
    /// 默认、无
    /// </summary>
    [Description("默认、无")]
    None = 0,

    /// <summary>
    /// HTTP
    /// </summary>
    [Description("HTTP")]
    HTTP,

    /// <summary>
    /// GRPC
    /// </summary>
    [Description("GRPC")]
    GRPC,
}