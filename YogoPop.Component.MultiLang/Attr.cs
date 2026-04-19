namespace YogoPop.Component.MultiLang;

[Injection(typeof(MultiLanguageInitAspect))]
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class MultiLanguageInitAttribute : DescriptionAttribute
{
    public string GroupKey { get; private set; }
    public string ItemKey { get; private set; }

    public MultiLanguageInitAttribute(string groupKey, string itemKey, string desc) : base(desc)
    {
        GroupKey = groupKey;
        ItemKey = itemKey;
    }
}

[Aspect(Scope.PerInstance)]
public class MultiLanguageInitAspect : AOPAspectAsyncBase
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
        try
        {
            var attr = source.GetType().GetCustomAttribute<MultiLanguageInitAttribute>();
            if (attr == null) return;

            var obj = source as MultiLanguageObject;
            if (obj == null) return;

            obj.GroupKey = attr.GroupKey;
            obj.ItemKey = attr.ItemKey;
        }
        catch (Exception ex)
        {
        }
    }
}