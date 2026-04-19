namespace YogoPop.Component.Permission;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
public abstract class PropertyPermissionBaseAttribute : PermissionPropertyBaseAttribute
{
    public PropertyPermissionBaseAttribute(string code, string parentCode, PermissionPropertyFailHandleEnum failHandle, params object[] defaultValues) : base(PermissionTypeEnum.Property, code, parentCode, failHandle, defaultValues) { }
}