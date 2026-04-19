namespace DForge.Infrastructure.DTO;

public class DTOSysAdminPage : DTOPager<DTOSort>
{
    [Description("用户名")]
    public string? UserName { get; set; }
}