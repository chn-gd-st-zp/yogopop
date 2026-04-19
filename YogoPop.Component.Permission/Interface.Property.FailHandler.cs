namespace YogoPop.Component.Permission;

public interface IPermissionPropertyFailHandler : ITransient
{
    public object Progress(IPermission permission, Attribute attr, object propertyValue);
}