namespace DForge.Infrastructure.DBSupport.Field;

public class DBFPaymentDomain : DBFSysSettings
{
    public string Resource { get; set; } = string.Empty;

    public string Tag { get; set; } = string.Empty;

    public string Main { get; set; } = string.Empty;

    public string Pay { get; set; } = string.Empty;

    public string ResourceUrl(string merchantCode) => Resource;

    public string MainUrl(string merchantCode) => Main.Replace(Tag, merchantCode);

    public string PayUrl(string merchantCode) => Pay.Replace(Tag, merchantCode);
}