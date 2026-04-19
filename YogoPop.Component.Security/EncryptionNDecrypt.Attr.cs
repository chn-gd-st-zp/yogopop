namespace YogoPop.Component.Security;

[Injection(typeof(DecryptAspect))]
[AttributeUsage(AttributeTargets.Property)]
public class DecryptAttribute : Attribute
{
    public EncryptionNDecryptEnum DecryptType { get; private set; }

    public EnvironmentEnum[] EEnvs { get; private set; }

    public DecryptAttribute(EncryptionNDecryptEnum decryptType, params EnvironmentEnum[] envs) { DecryptType = decryptType; EEnvs = envs; }
}

[Aspect(Scope.Global)]
public class DecryptAspect : AOPAspectAsyncBase
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

    protected override async Task<object> After(object source, MethodInfo methodInfo, Attribute[] triggers, string actionName, object[] actionParams, object actionResult)
    {
        if (
            methodInfo.IsSpecialName
            && methodInfo.Name.Contains("_get_", StringComparison.OrdinalIgnoreCase)
            && actionResult != null
        )
        {
            var attr = triggers.Where(o => o is DecryptAttribute).FirstOrDefault();
            if (attr != null)
            {
                var realAttr = attr as DecryptAttribute;
                if (realAttr.EEnvs.Contains(AppInitHelper.Environment) && InjectionContext.Root != null)
                {
                    using var diScope = InjectionContext.Root.CreateScope();
                    var encryptionNDecrypt = diScope.ResolveByKeyed<IEncryptionNDecrypt>(realAttr.DecryptType);
                    if (encryptionNDecrypt != null)
                    {
                        try
                        {
                            actionResult = encryptionNDecrypt.Decrypt(actionResult.ToString());
                        }
                        catch (Exception)
                        {
                            return actionResult.ToString();
                        }
                    }
                }
            }
        }

        return actionResult;
    }
}