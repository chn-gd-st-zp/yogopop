namespace YogoPop.Component.Swagger;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
public partial class PropertyDescriptionAttribute : Attribute
{
    public string Name { get; set; }

    public Type OutputType { get; set; }

    public Type PropertiesHiddenIfWithAttr { get; set; }

    public PropertyDescriptionAttribute(string name, Type type, Type propertiesHiddenIfWithAttr = null) { Name = name; OutputType = type; PropertiesHiddenIfWithAttr = propertiesHiddenIfWithAttr; }
}