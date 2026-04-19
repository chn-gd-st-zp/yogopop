namespace YogoPop.Core.Extension;

public static class TypeExtension
{
    public static bool IsString(this Type type) => type == typeof(string) || type == typeof(String) || type == typeof(StringValues);

    public static bool IsGenericOf(this Type type, Type genericType)
    {
        // 匹配接口。
        var isTheRawGenericType = type.GetInterfaces().Any(o => genericType == (o.IsGenericType ? o.GetGenericTypeDefinition() : o));
        if (isTheRawGenericType)
            return true;

        // 匹配类型。
        while (type != typeof(object))
        {
            isTheRawGenericType = genericType == (type.IsGenericType ? type.GetGenericTypeDefinition() : type);
            if (isTheRawGenericType)
                return true;

            type = type.BaseType;
        }

        // 没有找到任何匹配的接口或类型。
        return false;
    }

    public static bool IsImplementedOf(this Type type, Type basicType, bool isAllowIndirect = true)
    {
        if (type == basicType) return true;

        // 获取当前类实现的接口
        var allInterfaces = type.GetInterfaces();

        // 当前类的基类实现的接口
        var baseInterfaces = type.BaseType?.GetInterfaces() ?? Array.Empty<Type>();

        // 直接实现的接口
        var directInterfaces = allInterfaces.Except(baseInterfaces).ToArray();

        var typeForCheckArray = isAllowIndirect ? allInterfaces : directInterfaces;
        if (typeForCheckArray.Contains(basicType))
            return true;

        foreach (var t in typeForCheckArray)
        {
            if (t.IsGenericType && basicType.IsGenericType && t.Name == basicType.Name && t.GetGenericTypeDefinition() == basicType.GetGenericTypeDefinition())
                return true;
        }

        return false;
    }

    public static bool IsImplementedOf<T>(this Type objType, bool isAllowIndirect = true) => objType.IsImplementedOf(typeof(T), isAllowIndirect);

    public static bool IsExtendOf(this Type type, Type basicType) => type == basicType || type.IsSubclassOf(basicType);

    public static bool IsExtendOf<T>(this Type objType) => objType.IsExtendOf(typeof(T));

    public static TAttr GetAttr<TAttr>(this Type type, string value) where TAttr : Attribute
    {
        foreach (var field in type.GetFields())
        {
            if (!field.Name.IsEquals(value))
                continue;

            return field.GetCustomAttribute<TAttr>();
        }

        return null;
    }

    //public static T Convert<T>(this object value) => (T)System.Convert.ChangeType(value, typeof(T));

    public static T Convert<T>(this object obj)
    {
        if (obj == null) return default;

        if (obj is T) return (T)obj;

        try
        {
            return (T)System.Convert.ChangeType(obj, typeof(T));
        }
        catch
        {
            return default;
        }
    }

    public static bool IsPropertyNullable(this PropertyInfo propertyInfo) => Nullable.GetUnderlyingType(propertyInfo.PropertyType) != null;
}