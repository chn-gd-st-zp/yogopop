namespace YogoPop.DB.Define;

[AttributeUsage(AttributeTargets.Enum)]
public class DeleteDeclareAttribute : Attribute
{
    public object DeleteTag { get; private set; }

    public DeleteDeclareAttribute(object deleteTag)
    {
        DeleteTag = deleteTag;
    }
}