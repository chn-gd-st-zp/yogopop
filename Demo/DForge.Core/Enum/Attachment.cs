namespace DForge.Core.Enum;

/// <summary>
/// 附件类型
/// </summary>
[PublicEnum]
public enum AttachmentEnum
{
    [Description("默认、无")]
    None,

    [Description("其他")]
    Other,

    [Description("富文本内容")]
    TextContent,

    [Description("图标")]
    Icon,
}