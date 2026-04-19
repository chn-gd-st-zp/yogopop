namespace YogoPop.Component.Attachment;

public class AttachmentBasicSetting
{
    public PathModeEnum PathMode { get; set; }

    public string PathAddr { get; set; }

    public List<AttachmentHandlerSetting> Handlers { get; set; }

    public Dictionary<string, string> ExtMapping { get; set; }
}