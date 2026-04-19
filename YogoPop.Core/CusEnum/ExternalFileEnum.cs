namespace YogoPop.Core.CusEnum;

/// <summary>
/// 外部文件类型
/// </summary>
[Description("外部文件类型")]
public enum ExternalFileEnum
{
    /// <summary>
    /// 默认、无
    /// </summary>
    [Description("默认、无")]
    None = 0,

    /// <summary>
    /// DLL
    /// </summary>
    [Description("DLL")]
    DLL,

    /// <summary>
    /// XML
    /// </summary>
    [Description("XML")]
    XML,
}