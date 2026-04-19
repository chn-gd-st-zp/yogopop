namespace YogoPop.Core.JsonExtend;

[AttributeUsage(AttributeTargets.Property)]
public class JsonExtPropAttribute : Attribute
{
    //
}

public static class JsonExtPropAttributeExtension
{
    public static string ToJsonStr(this object obj)
    {
        var dic = new Dictionary<string, object>();

        if (obj == null)
            return dic.ToJson();

        obj.GetType().GetProperties()
            .Where(o => o.GetCustomAttribute<JsonExtPropAttribute>() != null)
            .ToList()
            .ForEach(o =>
            {
                var propertyName = o.Name;

                var attr = o.GetCustomAttribute<JsonPropertyAttribute>();
                if (attr != null)
                    propertyName = attr.PropertyName;

                dic.Add(propertyName, o.GetValue(obj));
            });

        return dic.ToJson();
    }
}