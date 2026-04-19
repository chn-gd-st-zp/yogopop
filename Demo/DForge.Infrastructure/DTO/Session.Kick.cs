namespace DForge.Infrastructure.DTO;

/// <summary>
/// 会话提出
/// </summary>
public class DTOSessionKick : DTOPrimaryKey<string>
{
    /// <summary>
    /// AccessToken
    /// </summary>
    [Description("AccessToken")]
    [JsonProperty("AccessToken"), PropertyRename("AccessToken")]
    [Required]
    public override string PrimaryKey { get; set; }
}