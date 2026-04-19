namespace DForge.Infrastructure.Permission.Attr;

[Injection(typeof(PermissionAspectForAction))]
public class ActionPermissionAttribute : ActionPermissionBaseAttribute
{
    public ActionPermissionAttribute(GlobalPermissionEnum code, GlobalPermissionEnum parentCode, Type inputType = null, Type dbDestinationType = null)
        : base(code.ToString(), parentCode.ToString(), inputType != null && dbDestinationType != null ? new PermissionMapping(inputType, dbDestinationType) : null) { }

    public ActionPermissionAttribute(GlobalPermissionEnum code, GlobalPermissionEnum parentCode, Type inputType, Type dbDestinationType, bool accessLogger)
        : base(code.ToString(), parentCode.ToString(), inputType != null && dbDestinationType != null ? new PermissionMapping(inputType, dbDestinationType) : null, accessLogger) { }
}