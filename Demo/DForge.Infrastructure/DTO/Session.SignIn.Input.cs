namespace DForge.Infrastructure.DTO;

/// <summary>
/// 会话登录
/// </summary>
public abstract class DTOSessionSignIn : DTOInput
{
    /// <summary>
    /// 角色类型
    /// </summary>
    [Description("角色类型")]
    [JsonIgnore, PropertyHidden]
    public abstract RoleEnum[] RoleTypes { get; }

    /// <summary>
    /// 入口
    /// </summary>
    [Description("入口")]
    public abstract EntryEnum Entry { get; set; }

    /// <summary>
    /// 账号（用户名 | 手机 | 邮箱）
    /// 如果是手机，要加上区号（如：+8613000045678）
    /// </summary>
    [Description("账号")]
    [Required]
    public string Account { get; set; }

    /// <summary>
    /// 密码（需先进行MD5加密，32位大写）
    /// </summary>
    [Description("密码")]
    [Required]
    public string Password { get; set; }

    /// <summary>
    /// MFA编码
    /// </summary>
    [Description("MFA编码")]
    public string? MFACode { get; set; }

    /// <summary>
    /// 参数校验
    /// </summary>
    /// <param name="errorMsg"></param>
    /// <returns></returns>
    public override bool Validation(out string errorMsg)
    {
        errorMsg = string.Empty;

        if (AccountHelper.IsUserName(Account))
        {
            //
        }
        else if (AccountHelper.IsMobile(Account))
        {
            //
        }
        else if (AccountHelper.IsEmail(Account))
        {
            //
        }
        else
        {
            errorMsg = GlobalSupport.CurLanguage.ToString().GetDestContent<ParameterError>();
            return false;
        }

        return true;
    }
}