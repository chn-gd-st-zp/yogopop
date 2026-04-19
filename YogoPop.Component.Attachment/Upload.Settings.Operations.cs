namespace YogoPop.Component.Attachment;

public class AttachmentOperationsSetting
{
    public string Key { get; set; }

    public List<AttachmentOperationItemSetting> Handlers { get; set; }
}

public class AttachmentOperationItemSetting
{
    public AttachmentHandlerEnum Handler { get; set; }

    public int MaxKB { get; set; } = 0;

    public string[] Args { get; set; } = new string[0];

    public List<TSettings> ParseArgs<TSettings>() where TSettings : class, AttachmentOperationArgSetting, new()
    {
        List<TSettings> result = new List<TSettings>();

        var props = typeof(TSettings).GetProperties();

        if (Args.IsEmpty())
            return result;

        foreach (var arg in Args)
        {
            TSettings resultItem = new TSettings();

            var paramArray = arg.SplitRemoveEmptyEntries('|');
            foreach (var param in paramArray)
            {
                var datas = param.SplitRemoveEmptyEntries(':');
                var key = datas[0];
                var value = datas[1];

                var prop = props.Where(o => o.Name == key).SingleOrDefault();
                if (prop == null)
                    continue;

                prop.SetValue(resultItem, value);
            }

            result.Add(resultItem);
        }

        return result;
    }
}