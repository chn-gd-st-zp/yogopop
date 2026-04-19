namespace YogoPop.Component.Permission;

public static class PermissionExtension
{
    public static Dictionary<string, string> AccessRecordCategory()
    {
        var result = new Dictionary<string, string>();

        var injectionSettings = InjectionContext.Resolve<InjectionSettings>();
        var dbContext = InjectionContext.Resolve<IDBContext>();

        AppInitHelper.GetAllType(injectionSettings.Patterns, injectionSettings.Dlls)
            .Where(o => o.IsClass && (o.IsImplementedOf<IAccessRecordTrigger>() || o.BaseType?.GetInterfaces().Contains(typeof(IAccessRecordTrigger)) == true))
            .ToList()
            .ForEach(o =>
            {
                var attr = o.GetCustomAttribute<TableAttribute>();
                var key = attr != null ? attr.Name : o.Name;
                var value = o.GetDesc();
                if (!result.ContainsKey(key)) result.Add(key, value);
            });

        return result;
    }
}