namespace YogoPop.Component.Attachment;

/// <summary>
/// 导出数据类型
/// </summary>
[Description("导出数据类型")]
public enum ExportDataTypeEnum
{
    /// <summary>
    /// 任意
    /// </summary>
    [Description("任意")]
    Any = 0,

    /// <summary>
    /// 时间
    /// </summary>
    [Description("时间")]
    DateTime,

    /// <summary>
    /// 整数
    /// </summary>
    [Description("整数")]
    Integer,

    /// <summary>
    /// 数值
    /// </summary>
    [Description("数值")]
    Decimals,
}