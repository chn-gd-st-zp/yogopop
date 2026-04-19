namespace YogoPop.Component.Dispatcher;

public class HangFireTimingSettings : TimingSettings<TimingParams> { }

public interface IHangFireTimingRegister : ITimingRegister<TimingParams> { }

//[DisableConcurrentExecution(30)]
public interface IHangFireTimingDispatcher : ITimingDispatcher { }

[DIModeForService(DIModeEnum.ExclusiveByKeyed, typeof(HangFireTimingDispatcher), DispatcherTypeEnum.HangFireTiming)]
public class HangFireTimingDispatcher : Dispatcher
{
    protected override void DoWork()
    {
        var settings = InjectionContext.Resolve<DispatcherSettings>();

        if (settings.HangFireTiming.IsNotEmpty())
        {
            foreach (var timer in settings.HangFireTiming)
            {
                foreach (var item in timer.Items)
                {
                    var register = InjectionContext.Resolve<IHangFireTimingRegister>();
                    var runner = InjectionContext.ResolveByNamed<IHangFireTimingDispatcher>(item.Type.IsNotEmptyString() ? item.Type : timer.Type);
                    register.Regis(runner, timer.Cron, item);
                }
            }
        }
    }
}

public class HangFireTimingRegister : IHangFireTimingRegister
{
    public async Task Regis(ITimingDispatcher runner, string cronOrDelayValue, TimingParams param)
    {
        switch (param.Timing)
        {
            case TimingEnum.Cron:
                RecurringJob.AddOrUpdate($"{param.Name}.{param.Timing}", () => runner.Run($"{param.Name}.{param.Timing}", param.Args), cronOrDelayValue, TimeZoneInfo.Local);
                //if (param.Timing == TimingEnum.Immediate)
                //    RecurringJob.Trigger(param.Name);
                break;
            case TimingEnum.Immediate:
                BackgroundJob.Enqueue(() => runner.Run($"{param.Name}.{param.Timing}", param.Args));
                break;
            case TimingEnum.Delay:
                BackgroundJob.Schedule(() => runner.Run($"{param.Name}.{param.Timing}", param.Args), TimeSpan.FromSeconds(double.Parse(cronOrDelayValue)));
                break;
            default:
                throw new NotSupportedException($"Timing type '{param.Timing}' is not supported.");
        }
    }
}