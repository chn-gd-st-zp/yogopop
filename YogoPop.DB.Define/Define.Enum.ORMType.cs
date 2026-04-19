namespace YogoPop.DB.Define;

/// <summary>
/// ORM类型
/// </summary>
[Description("ORM类型")]
public enum ORMTypeEnum
{
    /// <summary>
    /// 默认、无
    /// </summary>
    [Description("默认、无")]
    None = 0,

    /// <summary>
    /// EF
    /// </summary>
    [Description("EF")]
    EF,

    /// <summary>
    /// SqlSugar
    /// </summary>
    [Description("SqlSugar")]
    SqlSugar,
}