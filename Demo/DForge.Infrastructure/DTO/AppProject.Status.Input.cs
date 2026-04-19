namespace DForge.Infrastructure.DTO;

public class DTOAppProjectStatus : DTOPrimaryKeyRequired<string>
{
    /// <summary>
    /// 状态
    /// </summary>
    [Description("状态")]
    public StatusEnum Status { get; set; }
}