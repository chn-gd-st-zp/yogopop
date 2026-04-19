namespace YogoPop.Component.Permission;

public interface IPermissionConvertor : ISingleton
{
    public IPermission Parse(PermissionBaseAttribute attr);
}