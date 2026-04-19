namespace YogoPop.Component.Permission;

public abstract class PermissionActionBaseAttribute : PermissionBaseAttribute
{
    public PermissionMapping MappingType { get; }

    public PermissionActionBaseAttribute(PermissionTypeEnum type, string code, string parentCode, PermissionMapping mappingType = null, bool? accessLogger = null) : base(type, code, parentCode)
    {
        Type = type;
        Code = code;
        ParentCode = parentCode;
        MappingType = mappingType;
        AccessLogger = mappingType != null;
        AccessLogger = accessLogger != null && accessLogger.HasValue ? accessLogger.Value : AccessLogger;
    }
}