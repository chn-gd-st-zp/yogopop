namespace YogoPop.Core.Extension;

public static class ObjectExtension
{
    public static object GetFieldValue(this object obj, string fieldName)
    {
        if (obj == null)
            return null;

        var type = obj.GetType();
        var property = type.GetProperties().Where(o => o.Name.IsEquals(fieldName)).SingleOrDefault();
        if (property == null)
            return null;

        return property.GetValue(obj);
    }

    public static bool In<T>(this T obj, params T[] objs) => objs.Contains(obj);

    public static bool NotIn<T>(this T obj, params T[] objs) => !objs.Contains(obj);

    public static bool IsNullOrDefault<TEnum>(this TEnum? data, TEnum defaultValue = default) where TEnum : struct, Enum => data == null || EqualityComparer<TEnum>.Default.Equals(data.Value, default);

    public static bool IsNotNullOrDefault<TEnum>(this TEnum? data, TEnum defaultValue = default) where TEnum : struct, Enum => !data.IsNullOrDefault(defaultValue);


    public static bool IsDefault(this int data, int defaultValue = default) => EqualityComparer<int>.Default.Equals(data, defaultValue);

    public static bool IsNotDefault(this int data, int defaultValue = default) => !data.IsDefault(defaultValue);

    public static bool IsNullOrDefault(this int? data, int defaultValue = default) => data == null || EqualityComparer<int>.Default.Equals(data.Value, defaultValue);

    public static bool IsNotNullOrDefault(this int? data, int defaultValue = default) => !data.IsNullOrDefault(defaultValue);


    public static bool IsDefault(this decimal data, decimal defaultValue = default) => EqualityComparer<decimal>.Default.Equals(data, defaultValue);

    public static bool IsNotDefault(this decimal data, decimal defaultValue = default) => !data.IsDefault(defaultValue);

    public static bool IsNullOrDefault(this decimal? data, decimal defaultValue = default) => data == null || EqualityComparer<decimal>.Default.Equals(data.Value, defaultValue);

    public static bool IsNotNullOrDefault(this decimal? data, decimal defaultValue = default) => !data.IsNullOrDefault(defaultValue);


    public static bool IsDefault(this float data, float defaultValue = default) => EqualityComparer<float>.Default.Equals(data, defaultValue);

    public static bool IsNotDefault(this float data, float defaultValue = default) => !data.IsDefault(defaultValue);

    public static bool IsNullOrDefault(this float? data, float defaultValue = default) => data == null || EqualityComparer<float>.Default.Equals(data.Value, defaultValue);

    public static bool IsNotNullOrDefault(this float? data, float defaultValue = default) => !data.IsNullOrDefault(defaultValue);


    public static bool IsDefault(this double data, double defaultValue = default) => EqualityComparer<double>.Default.Equals(data, defaultValue);

    public static bool IsNotDefault(this double data, double defaultValue = default) => !data.IsDefault(defaultValue);

    public static bool IsNullOrDefault(this double? data, double defaultValue = default) => data == null || EqualityComparer<double>.Default.Equals(data.Value, defaultValue);

    public static bool IsNotNullOrDefault(this double? data, double defaultValue = default) => !data.IsNullOrDefault(defaultValue);
}