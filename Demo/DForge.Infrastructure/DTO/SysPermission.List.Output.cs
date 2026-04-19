namespace DForge.Infrastructure.DTO;

public class DTOSysPermissionListResult : DTOSysPermissionResult
{
    [JsonIgnore, PropertyHidden]
    public override string ParentNode { get; set; }

    [JsonIgnore, PropertyHidden]
    public override string FullNode { get; set; }

    [JsonIgnore, PropertyHidden]
    public override string CurSequence { get; set; }

    [JsonIgnore, PropertyHidden]
    public override string FullSequence { get; set; }
}