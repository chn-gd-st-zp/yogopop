namespace DForge.Infrastructure.DTO;

public class DTOSysRoleUpdate : DTOPrimaryKeyRequired<string>
{
    /// <summary>
    /// 名称
    /// </summary>
    [Description("名称")]
    [Required]
    public string Name { get; set; }

    /// <summary>
    /// 权限集合 - 要添加的
    /// </summary>
    [Description("权限集合 - 要添加的")]
    public string[]? PermissionCodes_Create { get; set; }

    /// <summary>
    /// 权限集合 - 要删除的
    /// </summary>
    [Description("权限集合 - 要删除的")]
    public string[]? PermissionCodes_Delete { get; set; }

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