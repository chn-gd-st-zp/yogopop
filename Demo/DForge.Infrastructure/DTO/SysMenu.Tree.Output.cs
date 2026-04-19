namespace DForge.Infrastructure.DTO;

public class DTOSysMenuTreeResult : DTOTreeNSequenceResult, IDTOPrimaryKey<string>
{
    /// <summary>
    /// ID
    /// </summary>
    [Description("ID")]
    [JsonProperty("ID"), PropertyRename("ID")]
    public virtual string PrimaryKey { get; set; }

    /// <summary>
    /// 菜单类型
    /// </summary>
    [Description("菜单类型")]
    public SysMenuEnum Type { get; set; } = SysMenuEnum.None;

    /// <summary>
    /// 名称
    /// </summary>
    [Description("名称")]
    public override string Name { get; set; } = string.Empty;

    /// <summary>
    /// 路由
    /// </summary>
    [Description("路由")]
    public string Route { get; set; } = string.Empty;

    /// <summary>
    /// 权限代码
    /// </summary>
    [Description("权限代码")]
    public string PermissionCode { get; set; } = string.Empty;

    [JsonIgnore, PropertyHidden]
    public override string ParentNode { get; set; }

    [JsonIgnore, PropertyHidden]
    public override string FullNode { get; set; }

    [JsonIgnore, PropertyHidden]
    public override string CurSequence { get; set; }

    [JsonIgnore, PropertyHidden]
    public override string FullSequence { get; set; }
}