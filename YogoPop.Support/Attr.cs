namespace YogoPop.Support;

[Injection(typeof(ApiVerAspect))]
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
public class ApiVerAttribute : Attribute
{
    public string LowestAdapt { get; private set; }

    public ApiVerAttribute(string lowestAdapt) { LowestAdapt = lowestAdapt; }
}

[Aspect(Scope.PerInstance)]
public class ApiVerAspect : AOPAspectBase
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

    protected override void Before(object source, MethodInfo methodInfo, Attribute[] triggers, string actionName, object[] actionParams)
    {
        var apiVerService = InjectionContext.Resolve<IApiVerService>();
        if (apiVerService == null) return;

        var attr = triggers.Where(o => o.GetType().IsExtendOf<ApiVerAttribute>()).Select(o => o as ApiVerAttribute).FirstOrDefault();
        if (attr == null) return;

        var requestVer = default(Version);
        var lowestVer = default(Version);

        try
        {
            requestVer = new Version(apiVerService.GetRequestVersion());
            lowestVer = new Version(attr.LowestAdapt);
        }
        catch
        {
            return;
        }

        if (requestVer.IsOlderThan(lowestVer))
            throw new VEApiVersionRestrict();
    }
}

public interface IApiVerService : ITransient
{
    public string GetRequestVersion();
}