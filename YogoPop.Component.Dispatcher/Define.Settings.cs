namespace YogoPop.Component.Dispatcher;

[DIModeForSettings("DispatcherSettings", typeof(DispatcherSettings))]
public class DispatcherSettings : ISettings
{
    public List<DispatcherTypeEnum> RunningServices { get; set; }

    public List<BackgroundSettings> Background { get; set; }

    public List<HangFireTimingSettings> HangFireTiming { get; set; }

    public List<QuartzTimingSettings> QuartzTiming { get; set; }
}

public class DispatcherSettingsItem
{
    public string Type { get; set; }
}