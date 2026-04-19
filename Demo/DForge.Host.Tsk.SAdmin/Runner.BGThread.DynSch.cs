namespace DForge.Host.SDispatcher;

[DIModeForService(DIModeEnum.ExclusiveByNamed, typeof(IBackgroundDispatcher), nameof(RDynSchConsumer))]
public class RDynSchConsumer : DomainForgeDispatcher<RDynSchConsumer>, IBackgroundDispatcher
{
    public async Task Run(string name, params string[] args)
    {
        RunnerName = $"{name}";
        Args = args;

        try
        {
            var dynSch = args[0].ToEnum<DynSchEnum>();

            InjectionContext.Resolve<DynSchConsumer>().RunAsync(dynSch);
        }
        catch (Exception ex)
        {
            Info($"Has Error");
            Error($"{ex.Message}", ex);
        }
    }
}