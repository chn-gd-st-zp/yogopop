namespace YogoPop.Component.PushNotification;

public interface IPushNotification : ITransient
{
    public Task<bool> Regis(string recipientIden, string deviceToken = default, string endPoint = default);

    public Task<bool> RegisMultiple(params string[] recipientIdenArray);

    public Task<bool> Delete(string recipientIden);

    public Task<bool> Publish(IPushNotificationMsg pnMsg);
}

public interface IPushNotificationMsg
{
    public string Title { get; set; }

    public string SubTitle { get; set; }

    public string Content { get; set; }

    public DateTime CreateTime { get; set; }
}

public interface IPushNotificationMsg<TPushNotificationExtension> : IPushNotificationMsg where TPushNotificationExtension : IPushNotificationExtension
{
    public TPushNotificationExtension Extension { get; set; }
}

public interface IPushNotificationExtension
{
    public string InitiatorIcon { get; set; }

    public string InitiatorName { get; set; }

    public string RelativeType { get; set; }

    public string RelativeID { get; set; }

    public string[] SourceIDArray { get; set; }

    public string[] RecipientIdenArray { get; set; }

    public string[] ExcludeIdenArray { get; set; }
}