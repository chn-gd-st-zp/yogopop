namespace DForge.Infrastructure.DTO;

public class DTOSysRolePageResult : DTOSysRoleResult
{
    [JsonIgnore, PropertyHidden]
    public override string ParentNode { get; set; }

    [JsonIgnore, PropertyHidden]
    public override string FullNode { get; set; }

    [JsonIgnore, PropertyHidden]
    public override string[] PermissionCodes { get; set; }
}