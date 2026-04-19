namespace DForge.Infrastructure.DTO;

public class DTOAppDSPChannelSyncApply : DTOPrimaryKeyRequired<string>
{
    /// <summary>
    /// 域名服务运行类型
    /// </summary>
    [Description("域名服务运行类型")]
    [Required]
    public DSOptEnum[] DSOpts { get; set; }
}