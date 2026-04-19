namespace DForge.Infrastructure.DTO;

public class DTOSysAdminUpdate : DTOPrimaryKeyRequired<string>
{
    /// <summary>
    /// 角色ID
    /// </summary>
    [Description("角色ID")]
    [Required]
    public string RoleID { get; set; } = string.Empty;

    /// <summary>
    /// 状态
    /// </summary>
    [Description("状态")]
    [Required]
    public StatusEnum Status { get; set; } = StatusEnum.Normal;

    /// <summary>
    /// 参数校验
    /// </summary>
    /// <param name="errorMsg"></param>
    /// <returns></returns>
    public override bool Validation(out string errorMsg)
    {
        errorMsg = string.Empty;

        if (Status.In(StatusEnum.None, StatusEnum.Delete))
        {
            errorMsg = GlobalSupport.CurLanguage.ToString().GetDestContent<ParameterError>();
            return false;
        }

        return true;
    }
}