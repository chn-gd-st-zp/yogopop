namespace DForge.Infrastructure.DTO;

public class DTOSysMenuUpdate : DTOPrimaryKeyRequired<string>
{
    /// <summary>
    /// 名称
    /// </summary>
    [Description("名称")]
    [Required]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 路由
    /// </summary>
    [Description("路由")]
    [Required]
    public string Route { get; set; } = string.Empty;

    /// <summary>
    /// 权限代码
    /// </summary>
    [Description("权限代码")]
    public string PermissionCode { get; set; } = string.Empty;
}