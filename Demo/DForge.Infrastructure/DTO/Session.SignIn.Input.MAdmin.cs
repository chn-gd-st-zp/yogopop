namespace DForge.Infrastructure.DTO;

/// <summary>
/// 会话登录
/// </summary>
public class DTOSessionMAdminSignIn : DTOSessionSignIn
{
    public override RoleEnum[] RoleTypes { get { return new RoleEnum[] { RoleEnum.User }; } }

    [JsonIgnore, PropertyHidden]
    public override EntryEnum Entry { get { return EntryEnum.PC; } set { } }
}