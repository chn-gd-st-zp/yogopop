namespace YogoPop.Core.CusEnum;

/// <summary>
/// 排序
/// </summary>
[Description("排序")]
public enum SortDirectionEnum
{
    /// <summary>
    /// 默认、无
    /// </summary>
    [Description("默认、无")]
    None = 0,

    /// <summary>
    /// 正序
    /// </summary>
    [Description("正序")]
    ASC = 1,

    /// <summary>
    /// 倒序
    /// </summary>
    [Description("倒序")]
    DESC = 2,
}