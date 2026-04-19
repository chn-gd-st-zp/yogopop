namespace DForge.Infrastructure.DTO;

public class DTOSysRoleCreate : DTOInput
{
    /// <summary>
    /// 类型
    /// </summary>
    [Description("类型")]
    [Required]
    public RoleEnum Type { get; set; } = RoleEnum.None;

    /// <summary>
    /// 子类型
    /// </summary>
    [Description("子类型")]
    public UserTypeEnum? SubType { get; set; } = UserTypeEnum.None;

    /// <summary>
    /// 名称
    /// </summary>
    [Description("名称")]
    [Required]
    public string Name { get; set; }

    /// <summary>
    /// 编码
    /// </summary>
    [Description("编码")]
    [JsonProperty("Code"), PropertyRename("Code")]
    [Required]
    public string CurNode { get; set; }

    /// <summary>
    /// 权限集合 - 要添加的
    /// </summary>
    [Description("权限集合 - 要添加的")]
    public string[]? PermissionCodes_Create { get; set; }
}