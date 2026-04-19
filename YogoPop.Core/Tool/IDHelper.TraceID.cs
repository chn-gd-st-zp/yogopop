namespace YogoPop.Core.Tool;

public class HttpTraceIDGenerator : IIdentityGenerator
{
    public string Get()
    {
        using (var diScope = InjectionContext.Root.CreateScope())
        {

            var context = diScope.Resolve<IHttpContextAccessor>();
            if (context == null || context.HttpContext == null)
                return Unique.GetGUID();

            return context.HttpContext.TraceIdentifier;
        }
    }
}