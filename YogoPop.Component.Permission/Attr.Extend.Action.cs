namespace YogoPop.Component.Permission;

[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = false)]
public abstract class ActionPermissionBaseAttribute : PermissionActionBaseAttribute
{
    public ActionPermissionBaseAttribute(string code, string parentCode, PermissionMapping mappingType = null, bool? accessLogger = null)
        : base(PermissionTypeEnum.Action, code, parentCode, mappingType, accessLogger) { }
}