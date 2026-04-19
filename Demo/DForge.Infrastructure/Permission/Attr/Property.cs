namespace DForge.Infrastructure.Permission.Attr;

[Injection(typeof(PermissionAspectForProperty))]
public class PropertyPermissionAttribute : PropertyPermissionBaseAttribute
{
    public PropertyPermissionAttribute(GlobalPermissionEnum code, GlobalPermissionEnum parentCode, PermissionPropertyFailHandleEnum failHandle, params object[] defaultValues) : base(code.ToString(), parentCode.ToString(), failHandle, defaultValues) { }
}