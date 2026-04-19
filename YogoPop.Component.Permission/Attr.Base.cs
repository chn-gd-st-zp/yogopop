namespace YogoPop.Component.Permission;

public abstract class PermissionBaseAttribute : Attribute
{
    public PermissionTypeEnum Type { get; protected set; }

    public string Code { get; protected set; }

    public string ParentCode { get; protected set; }

    public bool AccessLogger { get; protected set; } = false;

    public PermissionBaseAttribute(PermissionTypeEnum type, string code)
    {
        Type = type;
        Code = code;
        ParentCode = string.Empty;
    }

    public PermissionBaseAttribute(PermissionTypeEnum type, string code, string parentCode)
    {
        Type = type;
        Code = code;
        ParentCode = parentCode;
    }

    public IPermission Convert()
    {
        var service = InjectionContext.Resolve<IPermissionConvertor>();
        return service.Parse(this);
    }
}