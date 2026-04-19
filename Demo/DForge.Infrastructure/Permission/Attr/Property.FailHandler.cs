namespace DForge.Infrastructure.Permission.Attr;

[DIModeForService(DIModeEnum.ExclusiveByKeyed, typeof(IPermissionPropertyFailHandler), PermissionPropertyFailHandleEnum.Mosaic)]
public class PermissionPropertyFailHandler : IPermissionPropertyFailHandler
{
    public object Progress(IPermission permission, Attribute attr, object propertyValue)
    {
        return MosaicHandler.Parse(propertyValue);
    }
}