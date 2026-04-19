namespace YogoPop.Component.DataVisibility;

public class DTOElementSourcePage : DTOPager<DTOSort>
{
    /// <summary>
    /// 关键字
    /// </summary>
    [Description("关键字")]
    public string? Keyword { get; set; } = string.Empty;
}