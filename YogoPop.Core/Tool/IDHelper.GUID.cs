namespace YogoPop.Core.Tool;

public class GUIDGenerator : IIdentityGenerator
{
    public bool ReplaceSplitCode { get; set; } = true;

    public string Get() => Unique.GetGUID(ReplaceSplitCode);
}

public static class GUIDGeneratorExtension
{
    public static void UseGUID(this ContainerBuilder containerBuilder, bool ReplaceSplitCode = true)
    {
        containerBuilder.Register(o => new GUIDGenerator { ReplaceSplitCode = ReplaceSplitCode }).As<IIdentityGenerator>().InstancePerDependency();
    }
}