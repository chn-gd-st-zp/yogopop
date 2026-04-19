namespace YogoPop.Component.Attachment;

[DIModeForSettings("AttachmentSettings", typeof(AttachmentSettings))]
public class AttachmentSettings : ISettings
{
    public AttachmentBasicSetting Basic { get; set; }

    public List<AttachmentOperationsSetting> Operations { get; set; }

    public List<AttachmentUsage> ToUsage()
    {
        var result = new List<AttachmentUsage>();

        foreach (var operation in Operations)
        {
            var resultItem = new AttachmentUsage { Name = operation.Key, Items = new List<AttachmentUsageItem>() };
            foreach (var handler in operation.Handlers)
            {
                resultItem.Items.Add(new AttachmentUsageItem
                {
                    Handler = handler.Handler,
                    Size = handler.MaxKB,
                    Exts = Basic.Handlers.Where(o => o.Handler == handler.Handler).Single().Exts,
                });
            }

            result.Add(resultItem);
        }

        return result;
    }
}

public class AttachmentUsage
{
    public string Name { get; set; }

    public List<AttachmentUsageItem> Items { get; set; }
}

public class AttachmentUsageItem
{
    public AttachmentHandlerEnum Handler { get; set; }

    public int Size { get; set; }

    public string[] Exts { get; set; }
}