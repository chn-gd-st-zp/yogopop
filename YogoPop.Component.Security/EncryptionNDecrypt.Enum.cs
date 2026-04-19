namespace YogoPop.Component.Security;

/// <summary>
/// 加解密类型
/// </summary>
[Description("加解密类型")]
public enum EncryptionNDecryptEnum
{
    /// <summary>
    /// 默认、无
    /// </summary>
    [Description("默认、无")]
    None = 0,

    /// <summary>
    /// AES
    /// </summary>
    [Description("AES")]
    AES,

    /// <summary>
    /// DES
    /// </summary>
    [Description("DES")]
    DES,
}