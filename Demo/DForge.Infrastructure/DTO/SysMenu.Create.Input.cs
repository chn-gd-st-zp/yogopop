namespace DForge.Infrastructure.DTO;

public class DTOSysMenuCreate : DTOInput
{
    /// <summary>
    /// 系统类型
    /// </summary>
    [Description("系统类型")]
    [Required]
    public SysCategoryEnum Category { get; set; } = SysCategoryEnum.None;

    /// <summary>
    /// 菜单类型
    /// </summary>
    [Description("菜单类型")]
    public SysMenuEnum Type { get; set; } = SysMenuEnum.None;

    /// <summary>
    /// 名称
    /// </summary>
    [Description("名称")]
    public string Name { get; set; } = string.Empty;

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

    /// <summary>
    /// 父节点编码
    /// </summary>
    [Description("父节点编码")]
    [JsonProperty("ParentCode"), PropertyRename("ParentCode")]
    [Required]
    public string ParentNode { get; set; } = string.Empty;

    public override bool Validation(out string errorMsg)
    {
        errorMsg = string.Empty;

        if (Category == SysCategoryEnum.None || Type == SysMenuEnum.None)
        {
            errorMsg = GlobalSupport.CurLanguage.ToString().GetDestContent<ParameterError>();
            return false;
        }

        return true;
    }
}