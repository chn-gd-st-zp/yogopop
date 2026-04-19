namespace YogoPop.Component.Attachment;

/// <summary>
/// 附件尺寸
/// </summary>
[Description("附件尺寸")]
public enum AttachmentPictureSizeEnum
{
    /// <summary>
    /// 默认、无
    /// </summary>
    [Description("默认、无")]
    None = 0,

    /// <summary>
    /// 大
    /// </summary>
    [Description("大")]
    Large,

    /// <summary>
    /// 中
    /// </summary>
    [Description("中")]
    Medium,

    /// <summary>
    /// 小
    /// </summary>
    [Description("小")]
    Small,
}