namespace DForge.Infrastructure.DTO;

public abstract class DTOSysPermissionSearch : DTOSearch<DTOSort>
{
    /// <summary>
    /// 当前账号角色集合
    /// </summary>
    [Description("当前账号角色集合")]
    [JsonIgnore, PropertyHidden]
    public string[]? CurrentAccountRoleCodes { get; set; }

    /// <summary>
    /// 角色代码
    /// 无传递参数，视为不指定角色，以当前登录角色为基准
    /// 有传递参数，视为要指定角色，以参数为基准
    /// </summary>
    [Description("角色代码")]
    public string[]? RoleCodes { get; set; }
}