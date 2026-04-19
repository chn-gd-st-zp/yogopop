namespace YogoPop.Component.Swagger;

public class JsonType
{
    public string Name { get; set; }

    public string Type { get; set; }

    public bool Nullable { get; set; }

    public string Description { get; set; }
}

public static class JsonTypeExtension
{
    public static string ToSwaggerDesc<TEnum>(bool ignoreDesc = false, params TEnum[] ignores) where TEnum : Enum
    {
        var datas = new List<JsonType>();

        var dic = EnumExtension.ToDictionary(typeof(TEnum));
        foreach (var kv in dic)
        {
            if (kv.Value[0].In(ignores.Select(o => o.ToString()).ToArray()))
                continue;

            datas.Add(new JsonType
            {
                Name = kv.Key.ToString(),
                Description = ignoreDesc ? kv.Value[0] : kv.Value.ToString(": "),
            });
        }

        var result = string.Empty;

        foreach (var data in datas)
        {
            result += result.IsNotEmptyString() ? "<br>" : string.Empty;
            //result += $"{Printor.PrintHtmlSpace(4)}{data.Name}={data.Description}";
            result += $"{Printor.PrintHtmlSpace(4)}{data.Description}";
        }

        return result;
    }

    public static string ToSwaggerDesc(this IEnumerable<PropertyDescriptionAttribute> attrs)
    {
        var result = string.Empty;

        foreach (var attr in attrs)
        {
            var paramsDesc = attr.ToSwaggerDesc();

            result += result.IsNotEmptyString() ? "<br>" : string.Empty;
            result += $"<br>[";
            result += $"<br>{Printor.PrintHtmlSpace(2)}in case of \"{attr.Name}\"";
            result += paramsDesc.IsNotEmptyString() ? $": <br>{paramsDesc}" : ": empty";
            result += $"<br>]";
        }

        return result;
    }

    public static string ToSwaggerDesc(this PropertyDescriptionAttribute attr)
    {
        var datas = new List<JsonType>();

        var properties = attr.OutputType.GetProperties().ToList();

        if (attr.PropertiesHiddenIfWithAttr != null)
            properties = properties.Where(o => o.GetCustomAttribute(attr.PropertiesHiddenIfWithAttr) == null).ToList();

        foreach (var property in properties)
        {
            datas.Add(new JsonType
            {
                Name = property.Name,
                Type = property.PropertyType.Name.ToString(),
                Nullable = property.IsPropertyNullable(),
                Description = property.GetDesc(),
            });
        }

        var result = string.Empty;

        foreach (var data in datas)
        {
            result += result.IsNotEmptyString() ? "<br>" : string.Empty;
            result += $"{Printor.PrintHtmlSpace(4)}{data.Name}  {data.Type}  {(data.Nullable ? "null" : "notnull")}  {data.Description}";
        }

        return result;
    }

    public static string ToSwaggerDesc(this Type type)
    {
        var datas = new List<JsonType>();

        var properties = type.GetProperties().ToList();
        foreach (var property in properties)
        {
            var name = property.Name;

            var attr = property.GetCustomAttribute<PropertyRenameAttribute>();
            if (attr != null)
                name = attr.Name;

            datas.Add(new JsonType
            {
                Name = name,
                Type = property.PropertyType.Name.ToString(),
                Nullable = property.IsPropertyNullable(),
                Description = property.GetDesc(),
            });
        }

        var result = string.Empty;

        foreach (var data in datas)
        {
            result += result.IsNotEmptyString() ? "<br>" : string.Empty;
            result += $"{Printor.PrintHtmlSpace(4)}{data.Name}  {data.Type}  {(data.Nullable ? "null" : "notnull")}  {data.Description}";
        }

        return result;
    }
}