namespace YogoPop.Core.Extension;

public static class DescExtension
{
    public static string GetDesc(this Enum em) => em.GetType().GetDesc(em.ToString());

    public static string GetDesc(this FieldInfo fieldInfo)
    {
        string result = string.Empty;

        var attr = fieldInfo.GetCustomAttributes<DescriptionAttribute>().FirstOrDefault();
        if (attr == null)
            return result;

        result = attr.Description;

        return result;
    }

    public static string GetDesc(this PropertyInfo property)
    {
        string result = string.Empty;

        var attr = property.GetCustomAttributes<DescriptionAttribute>().FirstOrDefault();
        if (attr == null)
            return result;

        result = attr.Description;

        return result;
    }

    public static string GetDesc(this Type type)
    {
        string result = string.Empty;

        var attr = type.GetCustomAttributes<DescriptionAttribute>().FirstOrDefault();
        if (attr == null)
            return result;

        result = attr.Description;

        return result;
    }

    public static string GetDesc(this Type type, string value)
    {
        string result = string.Empty;

        var fi = type.GetField(value);
        if (fi == null)
            return result;

        var attr = fi.GetCustomAttributes<DescriptionAttribute>().FirstOrDefault();
        if (attr == null)
            return result;

        result = attr.Description;

        return result;
    }

    public static string GetDesc<T>(string value) => GetDesc(typeof(T), value);
}