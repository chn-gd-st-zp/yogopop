namespace YogoPop.Component.MultiLang;

/// <summary>
/// 多语言数据来源
/// </summary>
[Description("多语言数据来源")]
public enum MultiLangSourceEnum
{
    /// <summary>
    /// 默认、无
    /// </summary>
    [Description("默认、无")]
    None = 0,

    /// <summary>
    /// 来自文件
    /// </summary>
    [Description("来自文件")]
    FromFile,

    /// <summary>
    /// 来自数据库
    /// </summary>
    [Description("来自数据库")]
    FromDB,
}

/// <summary>
/// 多语言数据类型
/// </summary>
[Description("多语言数据来源")]
[InternalEnum]
public enum MultiLangMappingEnum
{
    /// <summary>
    /// 默认、无
    /// </summary>
    [Description("默认、无")]
    None = 0,

    /// <summary>
    /// 资源
    /// </summary>
    [Description("资源")]
    Resource,

    /// <summary>
    /// 提示
    /// </summary>
    [Description("提示")]
    Alert,

    /// <summary>
    /// 数据库数据
    /// </summary>
    [Description("数据库数据")]
    DBData,
}