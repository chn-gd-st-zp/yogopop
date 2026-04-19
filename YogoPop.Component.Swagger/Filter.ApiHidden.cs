namespace YogoPop.Component.Swagger;

public class ApiHiddenFilter : IDocumentFilter
{
    public void Apply(OpenApiDocument doc, DocumentFilterContext context)
    {
        foreach (ApiDescription apiDescription in context.ApiDescriptions)
        {
            var descriptor = apiDescription.ActionDescriptor as ControllerActionDescriptor;
            if (descriptor == null)
                return;

            if (descriptor.ControllerTypeInfo.GetCustomAttribute<ApiHiddenAttribute>() != null)
            {
                var controllerName = apiDescription.ActionDescriptor.RouteValues["controller"];

                var paths = doc.Paths.Where(o => o.Key.Contains($"/{controllerName}/", StringComparison.OrdinalIgnoreCase)).Select(o => o.Key).ToList();
                foreach (var path in paths)
                    doc.Paths.Remove(path);

                continue;
            }

            if (apiDescription.TryGetMethodInfo(out MethodInfo method))
            {
                if (method.CustomAttributes.Any(o => o.AttributeType == typeof(ApiHiddenAttribute)))
                {
                    string routeKey = "/" + apiDescription.RelativePath;
                    if (routeKey.Contains("?"))
                    {
                        int idx = routeKey.IndexOf("?", StringComparison.Ordinal);
                        routeKey = routeKey.Substring(0, idx);
                    }
                    doc.Paths.Remove(routeKey);
                }
            }
        }

        if (doc.Tags.IsNotEmpty())
        {
            var removeTags = new List<OpenApiTag>();

            foreach (var tag in doc.Tags)
            {
                var routePath = $"/{tag}/";
                if (!doc.Paths.Any(o => o.Key.Contains(routePath)))
                    removeTags.Add(tag);
            }

            foreach (var tag in removeTags)
                doc.Tags.Remove(tag);
        }
    }
}