namespace YogoPop.Core.VException;

[Description("自定义错误")]
public interface IVException
{
    public IStateCode BaseType { get; }

    public IVEnumItem Code { get; }

    public string VMessage { get; }
}

public abstract class VEBase : Exception, IVException
{
    public IStateCode BaseType { get; protected set; }

    public IVEnumItem Code { get; protected set; }

    public new string VMessage { get; protected set; }
}

public static class VExceptionExtension
{
    public static string GetMessage(this IVException vException)
    {
        var type = vException.BaseType.GetType();

        if (!type.IsImplementedOf<IStateCode>())
            return string.Empty;

        foreach (var i in type.GetInterfaces().Where(o => o == typeof(IStateCode) || o.IsImplementedOf<IStateCode>()))
        {
            foreach (var property in i.GetProperties())
            {
                if (property.Name != vException.Code.Name)
                    continue;

                var attr = property.GetCustomAttribute<DescriptionAttribute>();
                if (attr == null)
                    continue;

                return attr.Description;
            }
        }

        return string.Empty;
    }
}