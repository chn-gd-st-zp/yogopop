namespace DForge.Host.Tsk.SAdmin;

[DIModeForService(DIModeEnum.ExclusiveByNamed, typeof(IHangFireTimingDispatcher), nameof(RDynSchCreator))]
[DisableConcurrentExecution(300)]
public class RDynSchCreator : DomainForgeDispatcher<RDynSchCreator>, IHangFireTimingDispatcher
{
    public async Task Run(string name, params string[] args)
    {
        RunnerName = $"{name}";
        Args = args;

        if (RunningTaskManager.IsExist(RunnerName))
        {
            Info($"Already Exist");
            return;
        }

        try
        {
            Info($"Starting");
            RunningTaskManager.Add(RunnerName);

            var dynSch = args[0].ToEnum<DynSchEnum>();
            var frequency = args[1].ToEnum<DateCycleEnum>();

            using (var scope = InjectionContext.Root.CreateScope())
            using (var repository = scope.Resolve<IDBRepository>())
            {
                var channels = await repository.DBContext.ListAsync<TBAppDSPChannel>(o => o.Status == Core.Enum.StatusEnum.Normal);
                if (channels.IsEmpty()) return;

                var msgs = channels.GenerateMsg(new DSOptEnum[] { DSOptEnum.Regist }, frequency);
                if (msgs.IsEmpty())
                {
                    Info($"GenerateMsg failed");
                    return;
                }

                InjectionContext.Resolve<DynSchPublisher>().RunAsync<TBAppDynSchRecord>(dynSch, msgs.ToArray());
            }
        }
        catch (Exception ex)
        {
            Info($"Has Error");
            Error($"{ex.Message}", ex);
        }
        finally
        {
            RunningTaskManager.Remove(RunnerName);
            Info($"Ending");
        }
    }
}