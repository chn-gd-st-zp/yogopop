namespace DForge.Infrastructure.DTO;

public class DTOSystemStatusGetResult : DTOOutput
{
    public SysStatusEnum Status { get; set; }
    
    public DTORange<DateTime>? Duration { get; set; }
}