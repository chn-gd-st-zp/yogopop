namespace DForge.Infrastructure.DTO;

/// <summary>
/// 系统设置
/// </summary>
public class DTOSysSettingsStatus : DTOPrimaryKeyRequired<string>
{
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