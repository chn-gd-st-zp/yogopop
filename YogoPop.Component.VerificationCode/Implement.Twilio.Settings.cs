namespace YogoPop.Component.VerificationCode;

public class TwilioSettings : IVCSettings
{
    public int DurationSecond { get; set; }

    public string AccountSid { get; set; }

    public string AuthToken { get; set; }

    public string Locale { get; set; }

    public List<TwilioChannel> Channels { get; set; }

    public List<TwilioService> Services { get; set; }
}

public class TwilioChannel
{
    public RemoteChannelEnum RemoteChannel { get; set; }

    public string Value { get; set; }
}

public class TwilioService
{
    public string EventKey { get; set; }

    public string ServiceSID { get; set; }
}