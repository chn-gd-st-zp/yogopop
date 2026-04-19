namespace DForge.Infrastructure.DTO;

/// <summary>
/// 修改密码
/// </summary>
public class DTOSessionUpdatePassword : DTOInput
{
    /// <summary>
    /// 新密码（需先进行MD5加密，32位大写）
    /// </summary>
    [Description("新密码")]
    [Required]
    public string NewPassword { get; set; }

    /// <summary>
    /// 旧密码（需先进行MD5加密，32位大写）
    /// </summary>
    [Description("旧密码")]
    [Required]
    public string OldPassword { get; set; }
}