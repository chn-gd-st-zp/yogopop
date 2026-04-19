namespace YogoPop.Support;

public class DTOSort : IDTOSort, ITransient
{
    /// <summary>
    /// 字段名
    /// </summary>
    [Description("字段名")]
    public string FieldName { get; set; }

    /// <summary>
    /// 排序方式
    /// 0、2: AES、正序
    /// 1、3: DESC、倒序
    /// </summary>
    [Description("排序方式")]
    public SortDirectionEnum Direction { get; set; }
}