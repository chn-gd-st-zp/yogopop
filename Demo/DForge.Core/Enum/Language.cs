namespace DForge.Core.Enum;

/// <summary>
/// 语言
/// </summary>
[PublicEnum]
public enum LanguageEnum
{
    /// <summary>
    /// 未知，未定义
    /// </summary>
    [Description("未知，未定义")]
    Unknown = -1,

    /// <summary>
    /// 默认、无
    /// </summary>
    [Description("默认、无")]
    None = 0,

    /// <summary>
    /// 中文
    /// </summary>
    [Description("中文")]
    CN = 1,

    /// <summary>
    /// 英文
    /// </summary>
    [Description("英文")]
    EN = 2,
}