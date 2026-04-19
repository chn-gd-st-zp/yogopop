namespace YogoPop.Core.CusEnum;

/// <summary>
/// ContentType
/// </summary>
[Description("ContentType")]
public enum ContentTypeEnum
{
    /// <summary>
    /// 默认、无
    /// </summary>
    [Description("默认、无")]
    None = 0,

    /// <summary>
    /// application/x-www-form-urlencoded
    /// </summary>
    [Description("application/x-www-form-urlencoded")]
    Form,

    /// <summary>
    /// application/json
    /// </summary>
    [Description("application/json")]
    Json,

    /// <summary>
    /// application/xml
    /// </summary>
    [Description("application/xml")]
    Xml,

    /// <summary>
    /// application/text
    /// </summary>
    [Description("application/text")]
    Text,
}