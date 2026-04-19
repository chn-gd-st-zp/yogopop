namespace DForge.Infrastructure.DTO;

public class DTOAppDSPChannelSyncPage : DTOAppDynSchRecordPage
{
    [JsonIgnore, PropertyHidden]
    public override DynSchEnum[] DynSchs => new DynSchEnum[] { DynSchEnum.DomainSync };

    /// <summary>
    /// 通道ID
    /// </summary>
    [Description("通道ID")]
    [JsonProperty("ChannelID"), PropertyRename("ChannelID")]
    public override string PrimaryKey { get; set; } = string.Empty;
}