namespace YogoPop.Component.Permission;

public class PermissionEnum : IPermissionEnum
{
    public Type EnumType { get; private set; }

    internal PermissionEnum(Type enumType) { EnumType = enumType; }
}