namespace YogoPop.Component.Swagger;

public static class SwaggerExtension
{
    private static string _splitChar = "-";
    private static Dictionary<string, Type> _typeMap = new Dictionary<string, Type>();

    internal static string CustomSchemaIdSelector(this Type type)
    {
        var key = MD5.Encrypt($"{type.Assembly.GetName()},{type.FullName}");
        var value = type;

        if (!_typeMap.ContainsKey(key, true)) _typeMap.Add(key, value);

        return $"{type.Name}{_splitChar}{key}";
    }

    internal static Type CustomSchemaIdRestorer(this string key)
    {
        var datas = key.SplitRemoveEmptyEntries(_splitChar);
        return _typeMap.GetValue(datas[1], null, true);
    }

    internal static string GetCustomSchemaId(this string key) => key;

    internal static Tuple<string, OpenApiSchema> GetSwaggerProperty(this OpenApiSchema schema, string propertyName)
    {
        var inputPropertyKey = string.Empty;
        var inputPropertySchema = default(OpenApiSchema);

        //遍历所有字段名标识
        foreach (var key in schema.Properties.Keys)
        {
            //找出与字段匹配的字段名标识
            if (!propertyName.IsEquals(key))
                continue;

            inputPropertyKey = key;
            inputPropertySchema = schema.Properties[inputPropertyKey];

            //找到立刻跳出循环
            break;
        }

        //找不到字段信息
        if (inputPropertyKey.IsEmptyString() || inputPropertySchema == default)
            return null;

        return new Tuple<string, OpenApiSchema>(inputPropertyKey, inputPropertySchema);
    }
}