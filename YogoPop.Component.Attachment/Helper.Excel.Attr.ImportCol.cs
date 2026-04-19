namespace YogoPop.Component.Attachment;

[AttributeUsage(AttributeTargets.Property)]
public class ImportColAttribute : Attribute
{
    public ImportColAttribute(Type colType, string colAlias)
    {
        ColType = colType;
        ColAlias = colAlias;
    }

    /// <summary>
    /// 数据类型
    /// </summary>
    public Type ColType { get; set; }

    /// <summary>
    /// 别名
    /// </summary>
    public string ColAlias { get; set; }
}