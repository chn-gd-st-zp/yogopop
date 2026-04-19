namespace YogoPop.Component.Logger;

[DIModeForSettings("SeriLoggerSettings", typeof(SeriLoggerSettings))]
public class SeriLoggerSettings : ISettings
{
    public PathModeEnum PathMode { get; set; }
    public string PathAddr { get; set; }
    public RollingInterval RollingInterval { get; set; }
    public bool RollOnFileSizeLimit { get; set; }
    public int FileSizeLimitMB { get; set; }
    public string Template { get; set; }
    public SeriLogToEmail ToEmail { get; set; }
}

public class SeriLogToEmail
{
    public bool Enable { get; set; }
    public LogEventLevel MinimumLevel { get; set; }
    public string[] Triggers { get; set; }
    public string MailServer { get; set; }
    public int Port { get; set; }
    public string SenderEmail { get; set; }
    public string[] CarbonCopyEmail { get; set; }
    public string[] ReceiverEmails { get; set; }
}