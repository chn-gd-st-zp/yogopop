namespace YogoPop.Component.VerificationCode;

[AttributeUsage(AttributeTargets.Field)]
public class RemoteChannelAttribute : Attribute
{
    public RemoteChannelEnum[] RemoteChannels { get; private set; }

    public RemoteChannelAttribute(params RemoteChannelEnum[] remoteChannels)
    {
        RemoteChannels = remoteChannels;
    }
}