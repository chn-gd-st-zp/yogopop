namespace DForge.Infrastructure.DTO;

public class DTOSessionSAdminSignInResult : DTOSessionResult
{
    /// <summary>
    /// 角色集合
    /// </summary>
    [Description("角色集合")]
    public string[] RoleCodes { get; set; }

    /// <summary>
    /// 权限集合
    /// </summary>
    [Description("权限集合")]
    public string[] PermissionCodes { get; set; }

    /// <summary>
    /// 菜单集合
    /// </summary>
    [Description("菜单集合")]
    public DTOTree<DTOSysMenuTreeResult> Menus { get; set; }
}