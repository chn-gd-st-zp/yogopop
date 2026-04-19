namespace YogoPop.Component.Swagger;

public class PropertyOperationFilter : IDocumentFilter
{
    public void Apply(OpenApiDocument doc, DocumentFilterContext context)
    {
        foreach (var schema in doc.Components.Schemas)
        {
            var type = schema.Key.CustomSchemaIdRestorer();
            if (type == null) continue;

            var exactPropertyType = type.IsArray ? type.GetElementType() : type;
            if (exactPropertyType.IsExtendOf(typeof(Enum)))
                EnumOperation(schema.Value, type);

            if (type.IsImplementedOf(typeof(IDTO)))
                PropertyOperation(schema.Value, type);

            var attr_descs = type.GetCustomAttributes<PropertyDescriptionAttribute>();
            if (attr_descs.IsNotEmpty())
                schema.Value.Description += $"<br>{attr_descs.ToSwaggerDesc()}";
        }
    }

    private void EnumOperation(OpenApiSchema inputTypeSchema, Type inputType)
    {
        inputTypeSchema.Enum = new List<IOpenApiAny>();
        foreach (var item in inputType.ToDictionary())
        {
            inputTypeSchema.Enum.Add(new OpenApiString(item.Value[0]));
            inputTypeSchema.Description += $"<br>{item.Key}:{item.Value[0]}-{item.Value[1]}";
        }
    }

    private void PropertyOperation(OpenApiSchema inputTypeSchema, Type inputType)
    {
        //遍历字段
        foreach (var inputProperty in inputType.GetProperties())
        {
            var swaggerProperty = inputTypeSchema.GetSwaggerProperty(inputProperty.Name);

            //找不到字段信息
            if (swaggerProperty == null)
                continue;

            var attr_descs = inputProperty.GetCustomAttributes<PropertyDescriptionAttribute>();
            if (attr_descs.IsNotEmpty())
                swaggerProperty.Item2.Description += $"<br>{attr_descs.ToSwaggerDesc()}";

            var attr_hid = inputProperty.GetCustomAttribute<PropertyHiddenAttribute>();
            if (attr_hid != null)
            {
                inputTypeSchema.Properties.Remove(swaggerProperty.Item1);
                continue;
            }

            //如果不存在重命名的标签就进入下个匹配
            var attr_ren = inputProperty.GetCustomAttribute<PropertyRenameAttribute>();
            if (attr_ren != null && !inputTypeSchema.Properties.ContainsKey(attr_ren.Name.FormatPropertyName()))
            {
                inputTypeSchema.Properties.Remove(swaggerProperty.Item1);
                inputTypeSchema.Properties.Add(attr_ren.Name.FormatPropertyName(), swaggerProperty.Item2);

                if (inputTypeSchema.Required.Contains(swaggerProperty.Item1))
                {
                    inputTypeSchema.Required.Remove(swaggerProperty.Item1);
                    inputTypeSchema.Required.Add(attr_ren.Name.FormatPropertyName());
                }

                continue;
            }
        }
    }
}