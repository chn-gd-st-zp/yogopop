namespace DForge.Infrastructure.DTO;

/// <summary>
/// 下拉框数据
/// </summary>
public class DTODropItem : DTOOutput
{
    /// <summary>
    /// 名称
    /// </summary>
    [Description("名称")]
    public virtual string Name { get; set; }

    /// <summary>
    /// 值
    /// </summary>
    [Description("值")]
    public virtual string Value { get; set; }
}