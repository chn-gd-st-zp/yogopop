namespace DForge.Infrastructure.DTO;

public class DTOAppDomainNSModify : DTOInput
{
    /// <summary>
    /// 域名ID
    /// </summary>
    [Description("域名ID")]
    [Required]
    public string[] DomainIDs { get; set; } = default;

    /// <summary>
    /// NS集
    /// </summary>
    [Description("NS集")]
    [Required]
    public string[] NameServers { get; set; } = default;
}