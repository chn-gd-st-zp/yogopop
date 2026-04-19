namespace YogoPop.Component.Attachment;

/// <summary>
/// 附件结果
/// </summary>
[Description("附件结果")]
public enum AttachmentResultEnum
{
    /// <summary>
    /// 默认、无
    /// </summary>
    [Description("默认、无")]
    None = 0,

    /// <summary>
    /// 成功
    /// </summary>
    [Description("成功")]
    Success,

    /// <summary>
    /// 错误
    /// </summary>
    [Description("错误")]
    Error,

    /// <summary>
    /// 空标识
    /// </summary>
    [Description("空标识")]
    EmptyKey,

    /// <summary>
    /// 空数据
    /// </summary>
    [Description("空数据")]
    EmptyData,

    /// <summary>
    /// 找不到相应的流程处理方式
    /// </summary>
    [Description("找不到相应的流程处理方式")]
    OperationNotFound,

    /// <summary>
    /// 不支持该类型的后缀名
    /// </summary>
    [Description("不支持该类型的后缀名")]
    ExtNotSupport,

    /// <summary>
    /// 找不到相应的控制处理器
    /// </summary>
    [Description("找不到相应的控制处理器")]
    HandlerNotFound,

    /// <summary>
    /// 文件大小超过限制
    /// </summary>
    [Description("文件大小超过限制")]
    OverSize,
}