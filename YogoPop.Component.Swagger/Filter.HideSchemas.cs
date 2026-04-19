namespace YogoPop.Component.Swagger;

public class HideSchemasFilter : IDocumentFilter
{
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        // 清空 Components.Schema，使 Swagger 不显示 Schemas 部分
        swaggerDoc.Components.Schemas.Clear();
    }
}