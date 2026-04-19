namespace DForge.Infrastructure.DTO;

public class DTOSysAdminCreate : DTOInput
{
    /// <summary>
    /// 角色ID
    /// </summary>
    [Description("角色ID")]
    [Required]
    public string RoleID { get; set; }

    /// <summary>
    /// 用户名
    /// </summary>
    [Description("用户名")]
    [Required]
    public string UserName { get; set; }

    /// <summary>
    /// 密码（需先进行MD5加密，32位大写）
    /// </summary>
    [Description("密码")]
    [Required]
    public string Password { get; set; }

    /// <summary>
    /// 参数校验
    /// </summary>
    /// <param name="errorMsg"></param>
    /// <returns></returns>
    public override bool Validation(out string errorMsg)
    {
        errorMsg = string.Empty;

        if (!base.Validation(out errorMsg))
            return false;

        if (UserName.IsNotEmptyString() && !AccountHelper.IsUserName(UserName))
        {
            errorMsg = GlobalSupport.CurLanguage.ToString().GetDestContent<UsernameIncorrectFormat>();
            return false;
        }

        return true;
    }
}