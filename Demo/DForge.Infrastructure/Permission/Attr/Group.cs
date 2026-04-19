namespace DForge.Infrastructure.Permission.Attr;

public class GroupPermissionAttribute : GroupPermissionBaseAttribute
{
    public GroupPermissionAttribute(GlobalPermissionEnum code)
        : base(code.ToString()) { }

    public GroupPermissionAttribute(GlobalPermissionEnum code, GlobalPermissionEnum parentCode)
        : base(code.ToString(), parentCode.ToString()) { }
}