namespace YogoPop.Component.Swagger;

public class CustomizeHeaderFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var descriptor = context.ApiDescription.ActionDescriptor as ControllerActionDescriptor;
        if (descriptor == null)
            return;

        var classAttrs = descriptor.ControllerTypeInfo.GetCustomAttributes<CustomizeHeaderAttribute>();

        var methodAttrs = descriptor.MethodInfo.GetCustomAttributes<CustomizeHeaderAttribute>();

        if (classAttrs.IsEmpty() && methodAttrs.IsEmpty())
            return;

        if (operation.Parameters == null)
            operation.Parameters = new List<OpenApiParameter>();

        if (classAttrs.IsNotEmpty())
        {
            foreach (var attr in classAttrs)
            {
                operation.Parameters.Add(
                    new OpenApiParameter
                    {
                        Name = attr.Name,
                        In = ParameterLocation.Header,
                    }
                );
            }
        }

        if (methodAttrs.IsNotEmpty())
        {
            foreach (var attr in methodAttrs)
            {
                operation.Parameters.Add(
                    new OpenApiParameter
                    {
                        Name = attr.Name,
                        In = ParameterLocation.Header,
                    }
                );
            }
        }
    }
}