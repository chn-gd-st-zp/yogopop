namespace YogoPop.Core.DTO;

[Aspect(AspectInjector.Broker.Scope.PerInstance)]
public class InputVerifyAspect : AOPAspectBase
{
    [Advice(Kind.Around)]
    public override object HandleMethod(
       [Argument(Source.Instance)] object source,
       [Argument(Source.Target)] Func<object[], object> method,
       [Argument(Source.Triggers)] Attribute[] triggers,
       [Argument(Source.Name)] string actionName,
       [Argument(Source.Arguments)] object[] actionParams
    )
    {
        return base.HandleMethod(source, method, triggers, actionName, actionParams);
    }

    protected override void Before(object source, MethodInfo methodInfo, Attribute[] triggers, string actionName, object[] actionParams)
    {
        actionParams.Verify();
    }
}