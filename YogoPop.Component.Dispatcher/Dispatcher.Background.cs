namespace YogoPop.Component.Dispatcher;

public interface IBackgroundDispatcher : IDispatcher { }

public class BackgroundSettings : DispatcherSettingsItem
{
    public string Name { get; set; }

    public string[] Args { get; set; }
}

[DIModeForService(DIModeEnum.ExclusiveByKeyed, typeof(BackgroundDispatcher), DispatcherTypeEnum.Background)]
public class BackgroundDispatcher : Dispatcher
{
    protected override void DoWork()
    {
        var settings = InjectionContext.Resolve<DispatcherSettings>();

        if (settings.Background.IsNotEmpty())
        {
            foreach (var bgWorker in settings.Background)
            {
                var runner = InjectionContext.ResolveByNamed<IBackgroundDispatcher>(bgWorker.Type);
                runner.Run(bgWorker.Name, bgWorker.Args);
            }
        }
    }
}