namespace DForge.Infrastructure.Permission;

[DIModeForService(DIModeEnum.AsImpl)]
public class PermissionConvertor : IPermissionConvertor
{
    public IPermission Parse(PermissionBaseAttribute attr)
    {
        var ePermission = attr.Code.ToEnum<GlobalPermissionEnum>();

        var result = new TBSysPermission
        {
            PrimaryKey = ((int)ePermission).ToString(),
            Type = attr.Type,
            Name = ePermission.GetDesc(),
            CurNode = attr.Code,
            ParentNode = attr.ParentCode,
            AccessLogger = attr.AccessLogger,
        };
        result.SetSequence(((int)ePermission).ToString());

        return result;
    }
}