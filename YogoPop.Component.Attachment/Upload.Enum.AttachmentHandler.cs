namespace YogoPop.Component.Attachment;

/// <summary>
/// 附件类型
/// </summary>
[Description("附件类型")]
public enum AttachmentHandlerEnum
{
    /// <summary>
    /// 默认、无
    /// </summary>
    [Description("默认、无")]
    None = 0,

    /// <summary>
    /// 图片
    /// </summary>
    [Description("图片")]
    PIC,

    /// <summary>
    /// 媒体
    /// </summary>
    [Description("媒体")]
    Media,

    /// <summary>
    /// 文件
    /// </summary>
    [Description("文件")]
    DOC,

    /// <summary>
    /// 包
    /// </summary>
    [Description("包")]
    PKG,
}