namespace YogoPop.Component.Swagger;

public class RemoveContentTypeFilter : IDocumentFilter
{
    /// <summary>
    /// 要移除的 Content-Type
    /// </summary>
    public List<string> ContentTypes { get; private set; }

    public RemoveContentTypeFilter(string[] contentTypes)
    {
        ContentTypes = contentTypes.IsNotEmpty() ? contentTypes.ToList() : new List<string>
        {
            "application/json-patch+json",
            "text/json",
            "application/*+json",
            "application/xml",
        };
    }

    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        foreach (var pathItem in swaggerDoc.Paths.Values)
        {
            foreach (var operation in pathItem.Operations.Values)
            {
                foreach (var contentType in ContentTypes)
                {
                    if (operation.RequestBody != null && operation.RequestBody.Content.ContainsKey(contentType))
                    {
                        operation.RequestBody.Content.Remove(contentType);
                    }
                }
            }
        }
    }
}