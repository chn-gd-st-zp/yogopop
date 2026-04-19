namespace DForge.Support;

[Injection(typeof(MaintainAspect))]
[AttributeUsage(AttributeTargets.Class)]
public class MaintainCtrlAttribute : Attribute
{
    //
}

[Aspect(Scope.PerInstance)]
public class MaintainAspect : AOPAspectAsyncBase
{
    [Advice(Kind.Around)]
    public new object HandleMethod(
       [Argument(Source.Instance)] object source,
       [Argument(Source.Target)] Func<object[], object> method,
       [Argument(Source.Triggers)] Attribute[] triggers,
       [Argument(Source.Name)] string actionName,
       [Argument(Source.Arguments)] object[] actionParams
    )
    {
        return base.HandleMethod(source, method, triggers, actionName, actionParams);
    }

    protected override async Task Before(object source, MethodInfo methodInfo, Attribute[] triggers, string actionName, object[] actionParams)
    {
        var nowStatus = await SysStatusManager.GetAsync();
        if (nowStatus.Status == SysStatusEnum.Maintaining)
            throw new VESysMaintaining();
    }
}