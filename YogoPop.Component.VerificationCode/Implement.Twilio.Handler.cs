namespace YogoPop.Component.VerificationCode;

public class TwilioHandler : VCHandler<TwilioSettings>
{
    public override string Provider { get { return "Twilio"; } }

    public override void Init() => TwilioClient.Init(Settings.AccountSid, Settings.AuthToken);

    public override IServiceResult<string> SendMessage(IVCEntity entity, string message = default)
    {
        var key = string.Empty;

        if (VCGlobalSettings.IsTestMode)
            return key.Success();

        var channelValue = Settings.Channels.Where(o => o.RemoteChannel == entity.RemoteChannel).Select(o => o.Value).SingleOrDefault();
        if (channelValue.IsEmptyString())
            return key.Fail("不支持的业务方法");

        var serviceSid = Settings.Services.Where(o => o.EventKey == entity.Event).Select(o => o.ServiceSID).SingleOrDefault();
        if (serviceSid.IsEmptyString())
            return key.Fail("不支持的服务ID");

        //var verification = VerificationResource.Create(
        //    pathServiceSid: "VA52e3e8366a8aff9a53a845cb52ad5a7a"
        //    , to: "+639772412075"
        //    , channel: "sms"
        //    , locale: "zh"
        //);

        var verification = VerificationResource.Create(
            pathServiceSid: serviceSid
            , to: entity.ReceiverPrefix + entity.ReceiverNum
            , channel: channelValue
            , locale: Settings.Locale
        );

        key = verification.Sid;

        return key.Success();
    }

    public override bool VerifyByRemote(IVCEntity entity, string verifyCode)
    {
        var result = true;

        if (VCGlobalSettings.IsTestMode)
            return result;

        var serviceSid = Settings.Services.Where(o => o.EventKey == entity.Event).Select(o => o.ServiceSID).SingleOrDefault();
        result = serviceSid.IsEmptyString() ? false : true;
        if (!result)
            return result;

        //var verification = VerificationCheckResource.Create(
        //    pathServiceSid: "VA52e3e8366a8aff9a53a845cb52ad5a7a"
        //    , to: "+639772412075"
        //    , code: "验证码"
        //);

        var verification = VerificationCheckResource.Create(
            pathServiceSid: serviceSid
            , to: entity.ReceiverPrefix + entity.ReceiverNum
            , code: verifyCode
        );

        result = verification.Valid.HasValue ? verification.Valid.Value : false;

        return result;
    }
}