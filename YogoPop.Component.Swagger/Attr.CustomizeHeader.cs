namespace YogoPop.Component.Swagger;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
public partial class CustomizeHeaderAttribute : Attribute
{
    public string Name { get; set; }

    public CustomizeHeaderAttribute(string name) { Name = name; }
}