namespace DForge.Infrastructure.DTO;

public class DTOAppDomainSyncPage : DTOAppDynSchRecordPage
{
    [JsonIgnore, PropertyHidden]
    public override DynSchEnum[] DynSchs => new DynSchEnum[] { DynSchEnum.NameServerSync, DynSchEnum.DNSSync, DynSchEnum.AnalyseSync };

    /// <summary>
    /// 域名ID
    /// </summary>
    [Description("域名ID")]
    [JsonProperty("DomainID"), PropertyRename("DomainID")]
    public override string PrimaryKey { get; set; } = string.Empty;
}