namespace DForge.Infrastructure.DTO;

public class DTOSysRoleSingleResult : DTOSysRoleResult
{
    [JsonIgnore, PropertyHidden]
    public override string ParentNode { get; set; }

    [JsonIgnore, PropertyHidden]
    public override string FullNode { get; set; }
}