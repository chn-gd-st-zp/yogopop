namespace YogoPop.DB.Define;

public static class DBExtension
{
    public static string GetPKPropertyName(this IDBFPrimaryKey obj) => "PrimaryKey";

    public static string GetPKPropertyName<T>(this IDBFPrimaryKey<T> obj) => nameof(IDBFPrimaryKey<T>.PrimaryKey);

    public static PropertyInfo GetPKPropertyInfo(this IDBFPrimaryKey obj) => obj.GetType().GetProperty(obj.GetPKPropertyName());

    public static object GetPKValue(this object obj)
    {
        var type = obj.GetType();

        if (!type.IsGenericOf(typeof(IDBFPrimaryKey<>)))
            return null;

        var obj2 = obj as IDBFPrimaryKey;
        if (obj2 == null) return null;

        //var field = type.GetProperty(obj2.GetPKPropertyName(), BindingFlags.Public | BindingFlags.DeclaredOnly | BindingFlags.IgnoreCase);
        var field = type.GetProperty(obj2.GetPKPropertyName());
        if (field == null)
            return null;

        return field.GetValue(obj);
    }

    public static string GetTBName<TEntity>(this IDBContext dBContext) where TEntity : class, IDBEntity, new() => dBContext.GetTBName(typeof(TEntity));

    public static string GetTBName(this IDBContext dBContext, Type dbType)
    {
        if (dbType == null)
            return string.Empty;

        var attr = dbType.GetCustomAttribute<TableAttribute>();
        if (attr == default)
            return dbType.Name;

        return attr.Name;
    }

    public static string GetTBName(this IDBEntity dbObj)
    {
        if (dbObj == null)
            return string.Empty;

        var dbType = dbObj.GetType();
        if (dbType == null)
            return string.Empty;

        var attr = dbType.GetCustomAttribute<TableAttribute>();
        if (attr == default)
            return dbType.Name;

        return attr.Name;
    }

    public static string GetPKName<TEntity>(this IDBContext dBContext) where TEntity : class, IDBEntity, new() => dBContext.GetPKName(typeof(TEntity));

    public static string GetPKName(this IDBContext dBContext, Type dbType)
    {
        if (dbType == null)
            return string.Empty;

        if (!dbType.IsImplementedOf(typeof(IDBFPrimaryKey<>)))
            return string.Empty;

        var obj = InstanceCreator.Create(dbType) as IDBFPrimaryKey;
        if (obj == null)
            return string.Empty;

        var property = obj.GetPKPropertyInfo();
        if (property == null)
            return string.Empty;

        var attr = property.GetCustomAttribute<ColumnAttribute>();
        if (attr == null)
            return property.Name;

        return attr.Name;
    }
}