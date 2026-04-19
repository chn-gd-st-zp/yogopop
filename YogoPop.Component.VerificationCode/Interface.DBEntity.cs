namespace YogoPop.Component.VerificationCode;

public interface IVCEntity : IDBEntity, IDBFPrimaryKey<string>
{
    public string Event { get; set; }

    public RemoteChannelEnum RemoteChannel { get; set; }

    public string Provider { get; set; }

    public string ReceiverPrefix { get; set; }

    public string ReceiverNum { get; set; }

    public string VerifyKey { get; set; }

    public DateTime CreateTime { get; set; }

    public DateTime ExpiredTime { get; set; }

    public bool IsEnable { get; set; }
}