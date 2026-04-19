namespace DForge.Infrastructure.DTO;

public class DTOSystemStatusSet : DTOInput
{
    [Required]
    public SysStatusEnum Status { get; set; }

    public DTORange<DateTime>? Duration { get; set; }

    public override bool Validation(out string errorMsg)
    {
        errorMsg = string.Empty;

        if (Status == SysStatusEnum.Maintaining && Duration == null)
        {
            //errorMsg = "参数错误";
            errorMsg = GlobalSupport.CurLanguage.ToString().GetDestContent<ParameterError>();
            return false;
        }

        return true;
    }
}