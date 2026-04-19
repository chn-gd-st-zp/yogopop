namespace YogoPop.Component.Attachment;

public class AttachmentResult
{
    public AttachmentResultEnum State { get; set; } = AttachmentResultEnum.None;

    public List<AttachmentItemResult> Items { get; set; }
}

public class AttachmentItemResult
{
    public AttachmentResultEnum State { get; set; } = AttachmentResultEnum.None;

    public string FilePath { get; set; } = string.Empty;
}