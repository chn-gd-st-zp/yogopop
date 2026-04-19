namespace DForge.DSP;

/// <summary>
/// 域名服务运行类型
/// </summary>
[PublicEnum]
public enum DSOptEnum
{
    /// <summary>
    /// 默认、无
    /// </summary>
    [Description("默认、无")]
    None,

    /// <summary>
    /// 注册
    /// </summary>
    [Description("注册")]
    Regist,

    /// <summary>
    /// 解析
    /// </summary>
    [Description("解析")]
    Analyse,
}