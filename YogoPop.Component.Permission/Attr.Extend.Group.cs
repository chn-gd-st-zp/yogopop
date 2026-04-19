namespace YogoPop.Component.Permission;

[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Class, AllowMultiple = true)]
public abstract class GroupPermissionBaseAttribute : PermissionBaseAttribute
{
    public GroupPermissionBaseAttribute(string code)
        : base(PermissionTypeEnum.Group, code) { }

    public GroupPermissionBaseAttribute(string code, string parentCode)
        : base(PermissionTypeEnum.Group, code, parentCode) { }
}