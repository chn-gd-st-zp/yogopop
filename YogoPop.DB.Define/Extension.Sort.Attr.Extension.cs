namespace YogoPop.DB.Define;

public static class SortAttributeExtension
{
    public static DefaultSortAttribute GetDefaultSortField<T>(this Type type)
    {
        var attr = type.GetCustomAttribute<DefaultSortAttribute>();
        if (attr != null)
            return attr;

        return null;
    }

    public static Tuple<PropertyInfo, string> GetSortField<T>(this object obj, IDTOSort sort)
    {
        Type type = typeof(T);
        PropertyInfo[] piArray = type.GetProperties();

        return piArray.GetSortField<T>(sort);
    }

    public static Tuple<PropertyInfo, string> GetSortField<T>(this PropertyInfo[] piArray, IDTOSort sort)
    {
        foreach (var pi in piArray)
        {
            if (pi.Name.IsEquals(sort.FieldName))
                return new Tuple<PropertyInfo, string>(pi, pi.Name);

            var attr = pi.GetCustomAttribute<SortAttribute>();
            if (attr == null)
                continue;

            if (attr.RealName.IsEquals(sort.FieldName))
                return new Tuple<PropertyInfo, string>(pi, pi.Name);

            if (attr.NickNames.Contains(sort.FieldName, StringComparer.OrdinalIgnoreCase))
                return new Tuple<PropertyInfo, string>(pi, pi.Name);
        }

        return null;
    }
}