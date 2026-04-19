namespace YogoPop.Core.Attr.Label;

[AttributeUsage(AttributeTargets.Enum | AttributeTargets.Field)]
public class OperationAttribute : Attribute
{
    public OperationTypeEnum OperationType { get; private set; }

    public OperationAttribute(OperationTypeEnum operationType)
    {
        OperationType = operationType;
    }
}