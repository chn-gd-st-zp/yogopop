namespace DForge.DynScheduling;

public static class DynSchMQSettings
{
    public static string Topic => "DynSch";
}

public class DynSchMQMsg : IRabbitMQMessageEntity
{
    public IDynSchRecordEntity Record { get; set; }

    public IDynSchMQMsgExtraData ExtraData { get; set; }
}

public interface IDynSchMQMsgExtraData { }

public interface IDynSchMQMsgConvertor : ITransient
{
    public DynSchMQMsg Restore(string msg);
}