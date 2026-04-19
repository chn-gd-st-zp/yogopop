namespace YogoPop.Core.CusEnum;

/// <summary>
/// 运行环境
/// </summary>
[Description("运行环境")]
public enum EnvironmentEnum
{
    /// <summary>
    /// 默认、无
    /// </summary>
    [Description("默认、无")]
    None = 0,

    /// <summary>
    /// 开发环境
    /// </summary>
    [Description("开发环境")]
    DEV,

    /// <summary>
    /// 测试环境
    /// </summary>
    [Description("测试环境")]
    SIT,

    /// <summary>
    /// 测试环境
    /// </summary>
    [Description("测试环境")]
    STA,

    /// <summary>
    /// 用户模拟环境
    /// </summary>
    [Description("用户模拟环境")]
    UAT,

    /// <summary>
    /// 沙盒环境
    /// </summary>
    [Description("沙盒环境")]
    SANDBOX,

    /// <summary>
    /// 生产环境
    /// </summary>
    [Description("生产环境")]
    PROD,
}