namespace YogoPop.Component.Auth;

[DIModeForService(DIModeEnum.ExclusiveByKeyed, typeof(IYogoSession), ProtocolEnum.HTTP)]
public class YogoSession4HTTP : YogoSession<HTTPTokenProvider> { }