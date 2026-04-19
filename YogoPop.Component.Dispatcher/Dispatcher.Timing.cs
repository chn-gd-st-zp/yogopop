namespace YogoPop.Component.Dispatcher;

public enum TimingEnum
{
    Cron,
    Delay,
    Immediate,
}

public class TimingParams
{
    public string Type { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public TimingEnum Timing { get; set; } = TimingEnum.Cron;

    public string[] Args { get; set; } = new string[0];
}

public class TimingSettings<TTimingParams> : DispatcherSettingsItem where TTimingParams : TimingParams
{
    public string Cron { get; set; }

    public List<TTimingParams> Items { get; set; }
}

public interface ITimingRegister<TTimingParams> where TTimingParams : TimingParams
{
    Task Regis(ITimingDispatcher runner, string cronOrDelayValue, TTimingParams param);
}

public interface ITimingDispatcher : IDispatcher { }