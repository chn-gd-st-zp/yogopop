namespace YogoPop.Component.Attachment;

[AttributeUsage(AttributeTargets.Property)]
public abstract class ExportColAttribute : Attribute
{
    public ExportColAttribute(int index, string title, Type dataType)
    {
        Index = index;
        Title = title;
        DataType = dataType;
        DataFormat = string.Empty;
        Prefix = string.Empty;
        Suffix = string.Empty;
        Enlargement = 1;
    }

    public ExportColAttribute(int index, string title, Type dataType, string dataFormat = default, string prefix = default, string suffix = default, int enlargement = 1)
    {
        Index = index;
        Title = title;
        DataType = dataType;
        DataFormat = dataFormat;
        Prefix = prefix;
        Suffix = suffix;
        Enlargement = enlargement;
    }

    /// <summary>
    /// 列索引
    /// </summary>
    public int Index { get; set; }

    /// <summary>
    /// 标题
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// 数据类型
    /// </summary>
    public Type DataType { get; set; }

    /// <summary>
    /// 格式化
    /// </summary>
    public string DataFormat { get; set; }

    /// <summary>
    /// 开始字符
    /// </summary>
    public string Prefix { get; set; }

    /// <summary>
    /// 结束字符
    /// </summary>
    public string Suffix { get; set; }

    /// <summary>
    /// 放大倍数
    /// </summary>
    public int Enlargement { get; set; }

    /// <summary>
    /// 数据类型
    /// </summary>
    public ExportDataTypeEnum ExportDataType
    {
        get
        {
            if (DataType.Equals(typeof(DateTime)))
                return ExportDataTypeEnum.DateTime;
            else if (DataType.Equals(typeof(int)))
                return ExportDataTypeEnum.Integer;
            else if (
                DataType.Equals(typeof(decimal))
                || DataType.Equals(typeof(float))
                || DataType.Equals(typeof(double))
            )
                return ExportDataTypeEnum.Decimals;
            else
                return ExportDataTypeEnum.Any;
        }
    }

    /// <summary>
    /// 获取值
    /// 填充到表格之前调用
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    internal string GetDataValue(object obj)
    {
        if (obj == null)
            return string.Empty;

        string result = string.Empty;

        switch (ExportDataType)
        {
            case ExportDataTypeEnum.DateTime:
                result = DataFormat.IsEmptyString() ?
                    ((DateTime)obj).ToString("yyyy-MM-dd HH:mm:ss") : ((DateTime)obj).ToString(DataFormat);
                break;
            case ExportDataTypeEnum.Integer:
                var val_int = (int)obj * Enlargement;
                result = DataFormat.IsEmptyString() ? val_int.ToString() : val_int.ToString(DataFormat);
                break;
            case ExportDataTypeEnum.Decimals:
                var val_dcm = (decimal)obj * Enlargement;
                result = DataFormat.IsEmptyString() ? val_dcm.ToString() : val_dcm.ToString(DataFormat);
                break;
            default:
                result = obj.ToString();
                break;
        }

        return Prefix + result + Suffix;
    }

    /// <summary>
    /// 计算值
    /// </summary>
    /// <param name="obj1"></param>
    /// <param name="obj2"></param>
    /// <returns></returns>
    internal object Calculate(object obj1, object obj2)
    {
        if (obj1 == null && obj2 == null)
            return null;
        else if (obj1 == null)
            return obj2;
        else if (obj2 == null)
            return obj1;

        return CalculateValue(obj1, obj2);
    }

    /// <summary>
    /// 计算值
    /// </summary>
    /// <param name="obj1"></param>
    /// <param name="obj2"></param>
    /// <returns></returns>
    protected virtual object CalculateValue(object obj1, object obj2)
    {
        return obj1;
    }
}