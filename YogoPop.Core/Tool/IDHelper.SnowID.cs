namespace YogoPop.Core.Tool;

public class SnowIDGenerator : IIdentityGenerator
{
    public string Get() => YitIdHelper.NextId().ToString();
}

public static class SnowIDGeneratorExtension
{
    public static void UseSnowID(this ContainerBuilder containerBuilder, byte digit = 10)
    {
        var machineName = Dns.GetHostName();
        var workId = (ushort)(Math.Abs(machineName.GetHashCode()) % 111);
        YitIdHelper.SetIdGenerator(new IdGeneratorOptions(workId) { WorkerIdBitLength = digit });

        containerBuilder.RegisterType<SnowIDGenerator>().As<IIdentityGenerator>().InstancePerDependency();
    }
}