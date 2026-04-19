namespace YogoPop.Component.Swagger;

public class AccessTokenFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var swaggerSettings = InjectionContext.Resolve<SwaggerSettings>();
        if (swaggerSettings == null)
            return;

        if (swaggerSettings.AccessTokenKeyInHeader.IsEmptyString())
            return;

        var descriptor = context.ApiDescription.ActionDescriptor as ControllerActionDescriptor;
        if (descriptor == null)
            return;

        var classAttr = descriptor.ControllerTypeInfo.GetCustomAttribute<AllowAnonymousAttribute>();
        if (classAttr != null)
            return;

        var methodAttr = descriptor.MethodInfo.GetCustomAttribute<AllowAnonymousAttribute>();
        if (methodAttr != null)
            return;

        if (operation.Parameters == null)
            operation.Parameters = new List<OpenApiParameter>();

        operation.Parameters.Add(
            new OpenApiParameter
            {
                Name = swaggerSettings.AccessTokenKeyInHeader,
                In = ParameterLocation.Header,
            }
        );
    }
}