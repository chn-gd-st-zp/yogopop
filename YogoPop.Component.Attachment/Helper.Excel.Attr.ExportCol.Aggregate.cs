namespace YogoPop.Component.Attachment;

public class ExportAggregateAttribute : ExportColAttribute
{
    public ExportAggregateAttribute(int index, string title, Type dataType)
        : base(index, title, dataType)
    {
        AggregateType = AggregateTypeEnum.None;
    }

    public ExportAggregateAttribute(int index, string title, Type dataType, string dataFormat = default, string prefix = default, string suffix = default, int enlargement = 1)
        : base(index, title, dataType, dataFormat, prefix, suffix, enlargement)
    {
        AggregateType = AggregateTypeEnum.None;
    }

    public ExportAggregateAttribute(int index, string title, Type dataType, AggregateTypeEnum aggregateType)
        : base(index, title, dataType)
    {
        AggregateType = aggregateType;
    }

    public ExportAggregateAttribute(int index, string title, Type dataType, AggregateTypeEnum aggregateType, string dataFormat = default, string prefix = default, string suffix = default, int enlargement = 1)
        : base(index, title, dataType, dataFormat, prefix, suffix, enlargement)
    {
        AggregateType = aggregateType;
    }

    /// <summary>
    /// 底部总计使用计算
    /// </summary>
    public AggregateTypeEnum AggregateType { get; set; }

    protected override object CalculateValue(object obj1, object obj2)
    {
        switch (AggregateType)
        {
            case AggregateTypeEnum.Normal:
                return obj1;
            case AggregateTypeEnum.Sum:
                switch (ExportDataType)
                {
                    case ExportDataTypeEnum.Integer:
                        return (int)obj1 + (int)obj2;
                    case ExportDataTypeEnum.Decimals:
                        return (decimal)obj1 + (decimal)obj2;
                    default:
                        return obj1;
                }
            default:
                return obj1;
        }
    }
}