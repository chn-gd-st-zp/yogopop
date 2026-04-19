namespace YogoPop.Component.Attachment;

/// <summary>
/// 总计类型
/// </summary>
[Description("总计类型")]
public enum AggregateTypeEnum
{
    /// <summary>
    /// 不参与总计
    /// </summary>
    [Description("不参与总计")]
    None = 0,

    /// <summary>
    /// 默认、即值直接输出
    /// </summary>
    [Description("值直接输出")]
    Normal,

    /// <summary>
    /// 列值总合
    /// </summary>
    [Description("列值总合")]
    Sum,
}