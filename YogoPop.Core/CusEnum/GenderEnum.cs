namespace YogoPop.Core.CusEnum;

/// <summary>
/// 性别
/// </summary>
[Description("性别")]
[PublicEnum]
public enum GenderEnum
{
    /// <summary>
    /// 默认、无
    /// </summary>
    [Description("默认、无")]
    None = 0,

    /// <summary>
    /// 保密
    /// </summary>
    [Description("保密")]
    Secret,

    /// <summary>
    /// 男性
    /// </summary>
    [Description("男性")]
    Male,

    /// <summary>
    /// 女性
    /// </summary>
    [Description("女性")]
    Female,
}