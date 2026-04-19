namespace YogoPop.Component.Auth;

public class YogoSessionContext : YogoSessionContextBase
{
    public override void RestoreContextID() => new HttpTraceIDGenerator().Get();
}