namespace DForge.Infrastructure.DTO;

public abstract class DTOSysAdminResult : DTOOutput, IDTOPrimaryKey<string>
{
    /// <summary>
    /// ID
    /// </summary>
    [Description("ID")]
    [JsonProperty("ID"), PropertyRename("ID")]
    public string PrimaryKey { get; set; }

    /// <summary>
    /// 角色类型
    /// </summary>
    [Description("角色类型")]
    public RoleEnum RoleType { get; set; }

    /// <summary>
    /// 角色ID
    /// </summary>
    [Description("角色ID")]
    public string RoleID { get; set; }

    /// <summary>
    /// 角色名称
    /// </summary>
    [Description("角色名称")]
    public string RoleName { get; set; }

    /// <summary>
    /// 用户名
    /// </summary>
    [Description("用户名")]
    public string UserName { get; set; }

    /// <summary>
    /// MFA密匙
    /// </summary>
    [Description("MFA密匙")]
    [JsonIgnore, PropertyHidden]
    public string MFASecret { get; set; } = string.Empty;

    /// <summary>
    /// 创建时间
    /// </summary>
    [Description("创建时间")]
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    [Description("状态")]
    public StatusEnum Status { get; set; }

    /// <summary>
    /// MFA二维码
    /// </summary>
    [Description("MFA二维码")]
    public string MFAQRCode => GoogleMFA.GenerateQRCode(MFASecret, UserName);
}