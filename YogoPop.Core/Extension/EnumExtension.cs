namespace YogoPop.Core.Extension;

public static class EnumExtension
{
    public static object ToEnum(this Type type, string value)
    {
        try
        {
            return Enum.Parse(type, value, true);
        }
        catch
        {
            throw;
        }
    }

    public static T ToEnum<T>(this int value) where T : Enum
    {
        try
        {
            return (T)Enum.ToObject(typeof(T), value);
        }
        catch
        {
            throw;
        }
    }

    public static T ToEnum<T>(this int value, T defVal) where T : Enum
    {
        try
        {
            return (T)Enum.ToObject(typeof(T), value);
        }
        catch
        {
            return defVal;
        }
    }

    public static T ToEnum<T>(this string value) where T : Enum
    {
        try
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }
        catch
        {
            throw;
        }
    }

    public static T ToEnum<T>(this string value, T defVal) where T : Enum
    {
        try
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }
        catch
        {
            return defVal;
        }
    }

    public static List<T> ToEnumList<T>() where T : Enum
    {
        List<T> result = new List<T>();

        try
        {
            string[] nameArray = Enum.GetNames(typeof(T));
            foreach (string name in nameArray)
                result.Add(ToEnum<T>(name));
        }
        catch
        {
            throw;
        }

        return result;
    }

    public static List<T> ToEnumList<T>(this string[] dataArray) where T : Enum
    {
        List<T> result = new List<T>();

        try
        {
            foreach (string data in dataArray)
                result.Add(ToEnum<T>(data));
        }
        catch
        {
            throw;
        }

        return result;
    }

    public static int ToInt<T>(this T type) where T : Enum => Convert.ToInt32(type);

    public static Dictionary<int, string[]> ToDictionary<T>() where T : Enum
    {
        return typeof(T).ToDictionary();
    }

    public static Dictionary<int, string[]> ToDictionary(this Type type)
    {
        var dic = new Dictionary<int, string[]>();

        var eValueArray = Enum.GetValues(type);
        foreach (var eValue in eValueArray)
        {
            var fi = type.GetField(eValue.ToString());
            if (fi == null)
                continue;

            var key = (int)eValue;
            var value = new string[] { eValue.ToString(), fi.GetDesc() };

            dic.Add(key, value);
        }

        return dic;
    }

    public static Dictionary<int, string[]> ToDictionary(this Type type, List<Type> withAttrs = null)
    {
        var dic = new Dictionary<int, string[]>();

        var eValueArray = Enum.GetValues(type);
        foreach (var eValue in eValueArray)
        {
            var fi = type.GetField(eValue.ToString());
            if (fi == null)
                continue;

            if (withAttrs != null && withAttrs.Any())
            {
                if (!fi.GetCustomAttributes().Select(o => o.GetType()).Any(o => withAttrs.Contains(o)))
                    continue;
            }

            var key = (int)eValue;
            var value = new string[] { eValue.ToString(), fi.GetDesc() };

            dic.Add(key, value);
        }

        return dic;
    }

    public static T GetAttribute<T>(this Enum enumValue) where T : Attribute
    {
        var result = default(T);
        result = enumValue
            .GetType().GetMember(enumValue.ToString()).First()
            .GetCustomAttributes<T>(inherit: false)
            .FirstOrDefault();
        return result;
    }

    public static List<T> GetAttributes<T>(this Enum enumValue) where T : Attribute
    {
        var result = default(List<T>);
        result = enumValue
            .GetType().GetMember(enumValue.ToString()).First()
            .GetCustomAttributes<T>(inherit: false)
            .ToList();
        return result.IsNotEmpty() ? result : new List<T>();
    }
}