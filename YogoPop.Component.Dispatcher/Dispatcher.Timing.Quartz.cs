namespace YogoPop.Component.Dispatcher;

public class QuartzTimingSettings : TimingSettings<TimingParams> { }

public interface IQuartzTimingRegister : ITimingRegister<TimingParams> { }

//[DisallowConcurrentExecution]
public interface IQuartzTimingDispatcher : ITimingDispatcher, IJob { }

[DIModeForService(DIModeEnum.ExclusiveByKeyed, typeof(QuartzTimingDispatcher), DispatcherTypeEnum.QuartzTiming)]
public class QuartzTimingDispatcher : Dispatcher
{
    protected override void DoWork()
    {
        var settings = InjectionContext.Resolve<DispatcherSettings>();

        if (settings.QuartzTiming.IsNotEmpty())
        {
            foreach (var timer in settings.QuartzTiming)
            {
                foreach (var item in timer.Items)
                {
                    var register = InjectionContext.Resolve<IQuartzTimingRegister>();
                    var runner = InjectionContext.ResolveByNamed<IQuartzTimingDispatcher>(timer.Type);
                    register.Regis(runner, timer.Cron, item);
                }
            }
        }
    }
}

public class QuartzTimingRegister : IQuartzTimingRegister
{
    private IScheduler _scheduler { get; set; }

    public QuartzTimingRegister()
    {
        _scheduler = InjectionContext.Resolve<ISchedulerFactory>().GetScheduler().GetAwaiter().GetResult();
        _scheduler.JobFactory = InjectionContext.Resolve<IJobFactory>();

        _scheduler.Start().GetAwaiter().GetResult();
    }

    public async Task Regis(ITimingDispatcher runner, string cronOrDelayValue, TimingParams param)
    {
        var type = runner.GetType();

        var job = JobBuilder
            .Create(type)
            .WithIdentity($"{param.Name}.{param.Timing}")
            .WithDescription(param.Args.IsEmpty() ? string.Empty : string.Join(",", param.Args))
            .Build();

        switch (param.Timing)
        {
            case TimingEnum.Cron:
                {
                    var cronTrigger = TriggerBuilder.Create()
                        .WithIdentity($"{param.Name}.{param.Timing}.Trigger")
                        .WithCronSchedule(cronOrDelayValue)
                        .Build();

                    await _scheduler.ScheduleJob(job, cronTrigger);
                }
                break;
            case TimingEnum.Immediate:
                {
                    //var cronTrigger = TriggerBuilder.Create()
                    //    .WithIdentity($"{param.FullName}.{param.Timing}")
                    //    .WithCronSchedule(cronOrDelayValue)
                    //    .Build();

                    var immediateTrigger = TriggerBuilder.Create()
                        .WithIdentity($"{param.Name}.{param.Timing}.Trigger")
                        .StartNow()
                        .WithSimpleSchedule(x => x.WithRepeatCount(0))
                        .Build();

                    await _scheduler.ScheduleJob(job, immediateTrigger);
                    //await _scheduler.ScheduleJob(job, new HashSet<ITrigger> { cronTrigger, immediateTrigger }, true);
                }
                break;

            case TimingEnum.Delay:
                {
                    var delayTrigger = TriggerBuilder.Create()
                        .WithIdentity($"{param.Name}.{param.Timing}.Trigger")
                        .StartAt(DateBuilder.FutureDate(Convert.ToInt32(double.Parse(cronOrDelayValue)), IntervalUnit.Second))
                        .WithSimpleSchedule(x => x.WithRepeatCount(0))
                        .Build();

                    await _scheduler.ScheduleJob(job, delayTrigger);
                }
                break;
            default:
                throw new NotSupportedException($"Timing type '{param.Timing}' is not supported.");
        }
    }
}

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class JobTypeAttribute : Attribute
{
    public Type Type { get; private set; }

    public JobTypeAttribute(Type type) { Type = type; }
}

public class JobFactory : IJobFactory
{
    public void ReturnJob(IJob job) { }

    public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
    {
        var attr = bundle.JobDetail.JobType.GetCustomAttribute<JobTypeAttribute>();
        if (attr == null || !attr.Type.IsImplementedOf<IQuartzTimingDispatcher>()) return null;

        return InjectionContext.Resolve(attr.Type) as IJob;
    }
}