namespace DForge.Infrastructure.DTO;

/// <summary>
/// 会话登录
/// </summary>
public class DTOSessionSAdminSignIn : DTOSessionSignIn
{
    public override RoleEnum[] RoleTypes { get { return new RoleEnum[] { RoleEnum.SuperAdmin, RoleEnum.Admin }; } }

    [JsonIgnore, PropertyHidden]
    public override EntryEnum Entry { get { return EntryEnum.PC; } set { } }
}