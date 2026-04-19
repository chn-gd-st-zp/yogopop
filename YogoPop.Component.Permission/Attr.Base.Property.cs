namespace YogoPop.Component.Permission;

public abstract class PermissionPropertyBaseAttribute : PermissionBaseAttribute
{
    public PermissionPropertyFailHandleEnum FailHandle { get; }

    public object[] DefaultValues { get; }

    public PermissionPropertyBaseAttribute(PermissionTypeEnum type, string code, string parentCode, PermissionPropertyFailHandleEnum failHandle, params object[] defaultValues)
        : base(type, code)
    {
        Type = type;
        Code = code;
        ParentCode = parentCode;
        FailHandle = failHandle;
        DefaultValues = defaultValues;
    }
}