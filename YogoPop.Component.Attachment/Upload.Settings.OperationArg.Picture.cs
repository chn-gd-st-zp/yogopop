namespace YogoPop.Component.Attachment;

public class AttachmentPictureOperationArgSetting : AttachmentOperationArgSetting
{
    public string Type { get; set; }

    public string Suffix { get; set; }

    public string ShrinkTo { get; set; }

    public string Width { get; set; }

    public string Height { get; set; }

    public AttachmentPictureSizeEnum Size { get { return Type.ToEnum<AttachmentPictureSizeEnum>(); } }
}