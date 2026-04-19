namespace DForge.Infrastructure.DTO;

public class DTOAppDomainSyncCreate : DTOPrimaryKeyRequired<string>
{
    /// <summary>
    /// 通道ID
    /// </summary>
    [Description("通道ID")]
    public string ChannelID { get; set; } = string.Empty;

    /// <summary>
    /// 托管模式 已经设置过的可以不传
    /// </summary>
    [Description("托管模式")]
    public string? Trusteeship { get; set; } = string.Empty;
}